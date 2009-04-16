using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Gardian.Utilities.ChecksumValidator
{

    /// <summary>
    /// Collection of tracing methods.
    /// </summary>
    public static class TracingUtilities
    {

        ///------------------------------------------------
        /// <summary>
        /// Build string that contains all available information
        /// about the specified exception (all nested exceptions,
        /// type of exception, mesage, stack).
        /// </summary>
        public static StringBuilder BuildExceptionReport(Exception ex)
        {
            return BuildExceptionReport(null, null, null, ex, null);
        }


        ///------------------------------------------------
        /// <summary>
        /// Build string that contains all available information
        /// about the specified exception (all nested exceptions,
        /// type of exception, mesage, stack).
        /// </summary>
        public static StringBuilder BuildExceptionReport(Exception ex, StringBuilder builder)
        {
            return BuildExceptionReport(null, null, null, ex, builder);
        }


        ///------------------------------------------------
        /// <summary>
        /// Build string that contains all available information
        /// about the specified exception (all nested exceptions,
        /// type of exception, mesage, stack).
        /// </summary>
        public static StringBuilder BuildExceptionReport(string header, string footer, string separator, Exception ex, StringBuilder builder)
        {
            //validate arguments
            if (null == builder)
            {
                builder = new StringBuilder(512);
            }
            if (null == header)
            {
                header = "~~~~~~~~~" + Environment.NewLine + "Exception report";
            }
            if (null == footer)
            {
                footer = "~~~~~~~~~";
            }
            if (null == separator)
            {
                separator = "---------";
            }

            //header
            LinePrinter linePrinter = new LinePrinter(' ', 0, builder);
            if (header.Length > 0)
            {
                TraverseStringLines(header, linePrinter.Print);
                linePrinter.Print(separator);
            }

            //body
            TraverseExceptionTree(
                ex,
                0, //starting level
                true, //first exception in list
                delegate(KeyValuePair<KeyValuePair<int, bool>, string> info) //section header printer
                {
                    //print section header
                    linePrinter.UpdateIndent(info.Key.Key);
                    if (info.Key.Value) //first section
                    {
                        linePrinter.Print(string.Empty);
                        linePrinter.Print("List of embedded exceptions:");
                        linePrinter.Print(string.Empty);
                    }
                    else
                    {
                        linePrinter.Print(separator);
                    }

                    //user-friendly message (if present)
                    if (string.IsNullOrEmpty(info.Value))
                    {
                        linePrinter.Print("<no user friendly message>");
                    }
                    else
                    {
                        linePrinter.PrintPrefix();
                        builder.Append("User-friendly message: ");
                        linePrinter.StartWithoutPrefix();
                        TraverseStringLines(info.Value, linePrinter.Print);
                    }
                },
                delegate(KeyValuePair<KeyValuePair<int, bool>, Exception> info) //exception detail printer
                {
                    //make sure indent is up-to-date
                    linePrinter.UpdateIndent(info.Key.Key);

                    //separator between exceptions (if needed)
                    if (info.Key.Value)
                    {
                        if (info.Key.Key > 0)
                        {
                            linePrinter.UpdateIndent(info.Key.Key - 1);
                            linePrinter.Print("Exception details:");
                            linePrinter.UpdateIndent(info.Key.Key);
                        }
                    }
                    else
                    {
                        linePrinter.Print(separator);
                    }

                    //exception details
                    if (null == info.Value)
                    {
                        linePrinter.Print("<no exception was thrown>");
                    }
                    else
                    {
                        linePrinter.PrintPrefix();
                        builder.Append(info.Value.GetType().FullName);
                        builder.Append(" - ");
                        linePrinter.StartWithoutPrefix();
                        TraverseStringLines(string.IsNullOrEmpty(info.Value.Message) ? "<no exception message>" : info.Value.Message, linePrinter.Print);

                        FileNotFoundException tex = info.Value as FileNotFoundException;
                        if (null != tex)
                        {
                            if (!string.IsNullOrEmpty(tex.FileName))
                            {
                                linePrinter.PrintPrefix();
                                builder.Append("FileNotFoundException.FileName: ");
                                linePrinter.StartWithoutPrefix();
                                TraverseStringLines(tex.FileName, linePrinter.Print);
                            }
                            if (!string.IsNullOrEmpty(tex.FusionLog))
                            {
                                linePrinter.PrintPrefix();
                                builder.Append("FileNotFoundException.FusionLog: ");
                                linePrinter.StartWithoutPrefix();
                                TraverseStringLines(tex.FusionLog, linePrinter.Print);
                            }
                        }
                        else
                        {
                            SqlException sex = info.Value as SqlException;
                            if (null != sex)
                            {
                                linePrinter.PrintPrefix();
                                builder.Append("SqlException.ErrorCode: ");
                                builder.Append(sex.ErrorCode);
                                builder.Append(" (0x");
                                builder.Append(sex.ErrorCode.ToString("X8", CultureInfo.InvariantCulture));
                                builder.AppendLine(")");

                                linePrinter.PrintPrefix();
                                builder.Append("SqlException.Errors.Count: ");
                                builder.AppendLine(sex.Errors.Count.ToString(CultureInfo.InvariantCulture));
                                builder.AppendLine();

                                int index = 0;
                                foreach (SqlError sqlError in sex.Errors)
                                {
                                    string indexStr = (index++).ToString(CultureInfo.InvariantCulture);

                                    linePrinter.PrintPrefix();
                                    builder.Append("SqlException.Errors[");
                                    builder.Append(indexStr);
                                    builder.Append("].Class: ");
                                    builder.AppendLine(sqlError.Class.ToString(CultureInfo.InvariantCulture));

                                    linePrinter.PrintPrefix();
                                    builder.Append("SqlException.Errors[");
                                    builder.Append(indexStr);
                                    builder.Append("].LineNumber: ");
                                    builder.AppendLine(sqlError.LineNumber.ToString(CultureInfo.InvariantCulture));

                                    if (!string.IsNullOrEmpty(sqlError.Message))
                                    {
                                        linePrinter.PrintPrefix();
                                        builder.Append("SqlException.Errors[");
                                        builder.Append(indexStr);
                                        builder.Append("].Message: ");
                                        linePrinter.StartWithoutPrefix();
                                        TraverseStringLines(sqlError.Message, linePrinter.Print);
                                    }

                                    linePrinter.PrintPrefix();
                                    builder.Append("SqlException.Errors[");
                                    builder.Append(indexStr);
                                    builder.Append("].Number: ");
                                    builder.Append(sqlError.Number.ToString(CultureInfo.InvariantCulture));
                                    builder.Append(" (0x");
                                    builder.Append(sqlError.Number.ToString("X8", CultureInfo.InvariantCulture));
                                    builder.AppendLine(")");

                                    if (!string.IsNullOrEmpty(sqlError.Procedure))
                                    {
                                        linePrinter.PrintPrefix();
                                        builder.Append("SqlException.Errors[");
                                        builder.Append(indexStr);
                                        builder.Append("].Procedure: ");
                                        linePrinter.StartWithoutPrefix();
                                        TraverseStringLines(sqlError.Procedure, linePrinter.Print);
                                    }

                                    if (!string.IsNullOrEmpty(sqlError.Server))
                                    {
                                        linePrinter.PrintPrefix();
                                        builder.Append("SqlException.Errors[");
                                        builder.Append(indexStr);
                                        builder.Append("].Server: ");
                                        linePrinter.StartWithoutPrefix();
                                        TraverseStringLines(sqlError.Server, linePrinter.Print);
                                    }

                                    if (!string.IsNullOrEmpty(sqlError.Source))
                                    {
                                        linePrinter.PrintPrefix();
                                        builder.Append("SqlException.Errors[");
                                        builder.Append(indexStr);
                                        builder.Append("].Source: ");
                                        linePrinter.StartWithoutPrefix();
                                        TraverseStringLines(sqlError.Source, linePrinter.Print);
                                    }

                                    linePrinter.PrintPrefix();
                                    builder.Append("SqlException.Errors[");
                                    builder.Append(indexStr);
                                    builder.Append("].State: ");
                                    builder.AppendLine(sqlError.State.ToString(CultureInfo.InvariantCulture));

                                    builder.AppendLine();
                                }
                            }
                            else
                            {
                                ExternalException eex = info.Value as ExternalException;
                                if (null != eex)
                                {
                                    linePrinter.PrintPrefix();
                                    builder.Append("ExternalException.ErrorCode: ");
                                    builder.Append(eex.ErrorCode);
                                    builder.Append(" (0x");
                                    builder.Append(eex.ErrorCode.ToString("X8", CultureInfo.InvariantCulture));
                                    builder.AppendLine(")");
                                }
                            }
                        }

                        TraverseStringLines(string.IsNullOrEmpty(info.Value.StackTrace) ? "<no stack trace>" : info.Value.StackTrace, linePrinter.Print);
                    }
                });

            //footer
            if (footer.Length > 0)
            {
                linePrinter.UpdateIndent(0);
                TraverseStringLines(footer, linePrinter.Print);
            }

            return builder;
        }


        //-------------------------------------------------
        /// <summary>
        /// Output exception report (from BuildExceptionReport)
        /// to .NET's diagnostic trace using the specified category
        /// to mark each output line.
        /// </summary>
        public static void TraceExceptionReport(string category, Exception ex)
        {
            TraceExceptionReport(null, null, null, category, ex);
        }


        //-------------------------------------------------
        /// <summary>
        /// Output exception report (from BuildExceptionReport)
        /// to .NET's diagnostic trace using the specified category
        /// to mark each output line.
        /// </summary>
        public static void TraceExceptionReport(string header, string footer, string separator, string category, Exception ex)
        {
            StringBuilder b = BuildExceptionReport(header, footer, separator, ex, null);
            TraceMultilineText(b.ToString(), category);
        }


        //-------------------------------------------------
        /// <summary>
        /// </summary>
        public static void TraceMultilineText(string text, string category)
        {
            TraverseStringLines(text, delegate(string s)
            {
                Trace.WriteLine(s, category);
            });
        }


        //-------------------------------------------------
        /// <summary>
        /// Traverse all exceptions that are hanging off the
        /// specified root exception, calling the action handler
        /// for each exception encountered (including the root
        /// exception). Callback receives level
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="level"></param>
        /// <param name="first"></param>
        /// <param name="sectionStartHandler"></param>
        /// <param name="exceptionHandler"></param>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Method signature was designed so that only standard .NET types are used (helpful when this class is included in PRun project directly for compile)")]
        [SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "We don't expect levels to rise beyond int boundaries :)")]
        public static void TraverseExceptionTree(Exception ex, int level, bool first, System.Action<KeyValuePair<KeyValuePair<int, bool>, string>> sectionStartHandler, System.Action<KeyValuePair<KeyValuePair<int, bool>, Exception>> exceptionHandler)
        {
            while (null != ex)
            {
                if (null != exceptionHandler) { exceptionHandler(new KeyValuePair<KeyValuePair<int, bool>, Exception>(new KeyValuePair<int, bool>(level, first), ex)); }
                if (null != ex.Data)
                {
                    int sectionLevel = level + 1;
                    int exceptionLevel = level + 2;
                    bool firstInSection = true;
                    ReflectionTypeLoadException loadException = ex as ReflectionTypeLoadException;
                    if (null != loadException)
                    {
                        foreach (Exception subex in loadException.LoaderExceptions)
                        {
                            if (null != sectionStartHandler) { sectionStartHandler(new KeyValuePair<KeyValuePair<int, bool>, string>(new KeyValuePair<int, bool>(sectionLevel, firstInSection), null)); }
                            TraverseExceptionTree(subex, exceptionLevel, true, sectionStartHandler, exceptionHandler);
                            firstInSection = false;
                        }
                    }
                    foreach (DictionaryEntry dictionaryEntry in ex.Data)
                    {
                        IEnumerable<Exception> subexCollection = dictionaryEntry.Value as IEnumerable<Exception>;
                        if (null != subexCollection)
                        {
                            foreach (Exception subex in subexCollection)
                            {
                                if (null != sectionStartHandler) { sectionStartHandler(new KeyValuePair<KeyValuePair<int, bool>, string>(new KeyValuePair<int, bool>(sectionLevel, firstInSection), null)); }
                                TraverseExceptionTree(subex, exceptionLevel, true, sectionStartHandler, exceptionHandler);
                                firstInSection = false;
                            }
                        }
                        else
                        {
                            IEnumerable<KeyValuePair<string, Exception>> subexCollection2 = dictionaryEntry.Value as IEnumerable<KeyValuePair<string, Exception>>;
                            if (null != subexCollection2)
                            {
                                foreach (KeyValuePair<string, Exception> pair in subexCollection2)
                                {
                                    if (null != sectionStartHandler) { sectionStartHandler(new KeyValuePair<KeyValuePair<int, bool>, string>(new KeyValuePair<int, bool>(sectionLevel, firstInSection), pair.Key)); }
                                    TraverseExceptionTree(pair.Value, exceptionLevel, true, sectionStartHandler, exceptionHandler);
                                    firstInSection = false;
                                }
                            }
                        }
                    }
                }
                ex = ex.InnerException;
                first = false;
            }
        }


        //-------------------------------------------------
        /// <summary>
        /// Invoke the specified delegate for each line that's
        /// contained in the provided value. E.g. value
        /// "hello(newline)world(newline)" would cause action
        /// to be called twice, once with "hello" and once
        /// with "world".
        /// </summary>
        public static void TraverseStringLines(string value, System.Action<string> action)
        {
            if (null != action && null != value)
            {
                int start = 0;
                while (true)
                {
                    int end = value.IndexOf(Environment.NewLine, start, StringComparison.Ordinal);
                    if (end >= 0)
                    {
                        action(value.Substring(start, end - start));
                        start = end + Environment.NewLine.Length;
                    }
                    else
                    {
                        if (0 == start)
                        {
                            action(value);
                        }
                        else
                        {
                            int finalSegmentLength = value.Length - start;
                            if (finalSegmentLength > 0)
                            {
                                action(value.Substring(start, finalSegmentLength));
                            }
                        }
                        break;
                    }
                }
            }
        }




        /**************************************************
        /* Private
        /**************************************************/

        //-------------------------------------------------
        /// <summary>
        /// </summary>
        private sealed class LinePrinter
        {
            public LinePrinter(char prefix, int indent, StringBuilder stringBuilder)
            {
                this._prefix = prefix;
                this._repeatCount = indent;
                this._stringBuilder = stringBuilder;
            }

            public void Print(string s)
            {
                if (this._noPrefix)
                {
                    this._noPrefix = false;
                }
                else
                {
                    this._stringBuilder.Append(this._prefix, this._repeatCount);
                }
                this._stringBuilder.AppendLine(s);
            }

            public void PrintPrefix()
            {
                if (this._repeatCount > 0)
                {
                    this._stringBuilder.Append(this._prefix, this._repeatCount);
                }
            }

            public void StartWithoutPrefix()
            {
                this._noPrefix = true;
            }

            public void UpdateIndent(int indent)
            {
                this._repeatCount = 4 * indent;
            }

            private readonly char _prefix;
            private int _repeatCount;
            private bool _noPrefix; //initialized to false by CLR
            private readonly StringBuilder _stringBuilder;
        }

    }

}