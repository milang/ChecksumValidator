// Author: Damien Guard, (c) 2006
// (http://damieng.com/blog/2006/08/08/Calculating_CRC32_in_C_and_NET)

using System;
using System.Security.Cryptography;

public sealed class CRC32 : HashAlgorithm
{

    /// <summary>
    /// Default polynomial used for CRC32 computation.
    /// </summary>
    public const UInt32 DefaultPolynomial = 0xedb88320;

    /// <summary>
    /// Default CRC32 computation seed.
    /// </summary>
    public const UInt32 DefaultSeed = 0xffffffff;

    private UInt32 _hash;
    private readonly UInt32 _seed;
    private readonly UInt32[] _table;
    private static UInt32[] DefaultTable;

    ///<summary>
    ///</summary>
    public CRC32()
    {
        this._table = InitializeTable(DefaultPolynomial);
        this._seed = DefaultSeed;
        Initialize();
    }

    ///<summary>
    ///</summary>
    public CRC32(UInt32 polynomial, UInt32 seed)
    {
        this._table = InitializeTable(polynomial);
        this._seed = seed;
        Initialize();
    }

    public override void Initialize()
    {
        this._hash = this._seed;
    }

    protected override void HashCore(byte[] buffer, int start, int length)
    {
        this._hash = CalculateHash(this._table, this._hash, buffer, start, length);
    }

    protected override byte[] HashFinal()
    {
        byte[] hashBuffer = UInt32ToBigEndianBytes(~this._hash);
        this.HashValue = hashBuffer;
        return hashBuffer;
    }

    public override int HashSize
    {
        get { return 32; }
    }

    /// <summary>
    /// </summary>
    public static UInt32 Compute(byte[] buffer)
    {
        return ~CalculateHash(InitializeTable(DefaultPolynomial), DefaultSeed, buffer, 0, buffer.Length);
    }

    /// <summary>
    /// </summary>
    public static UInt32 Compute(UInt32 seed, byte[] buffer)
    {
        return ~CalculateHash(InitializeTable(DefaultPolynomial), seed, buffer, 0, buffer.Length);
    }

    /// <summary>
    /// </summary>
    public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
    {
        return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
    }

    private static UInt32[] InitializeTable(UInt32 polynomial)
    {
        if (polynomial == DefaultPolynomial && DefaultTable != null)
            return DefaultTable;

        var createTable = new UInt32[256];
        for (int i = 0; i < 256; i++)
        {
            var entry = (UInt32)i;
            for (int j = 0; j < 8; j++)
                if ((entry & 1) == 1)
                    entry = (entry >> 1) ^ polynomial;
                else
                    entry = entry >> 1;
            createTable[i] = entry;
        }

        if (polynomial == DefaultPolynomial)
            DefaultTable = createTable;

        return createTable;
    }

    private static UInt32 CalculateHash(UInt32[] table, UInt32 seed, byte[] buffer, int start, int size)
    {
        UInt32 crc = seed;
        for (int i = start; i < size; i++)
            unchecked
            {
                crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
            }
        return crc;
    }

    private static byte[] UInt32ToBigEndianBytes(UInt32 x)
    {
        return new[] {
			(byte)((x >> 24) & 0xff),
			(byte)((x >> 16) & 0xff),
			(byte)((x >> 8) & 0xff),
			(byte)(x & 0xff)
		};
    }
}