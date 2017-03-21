using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using Cereal64.Common.Rom;
using System.Xml.Linq;
using Cereal64.Common.Utils;

namespace Pitstop64.Data.Karts
{
    /// <summary>
    /// Thanks to Peter Lemon (krom) for notating the data in this block
    /// </summary>
    public class KartInformationBlock : N64DataElement
    {
        //ALL VALUES ARE Mario,Luigi,Yoshi,Toad,DK,Wario,Peach,Bowser

        public static int DefaultKartInformationBlock0Location = 0x0E2F60;
        public static int DefaultKartInformationBlock0End = 0x0E4410;

        public const int CHARACTER_COUNT = 8;
        public const int UNKNOWN_BLOCK_SIZE = 15;
        public const int ACCEL_BLOCK_SIZE = 10;
        public const int FILLER_5_SIZE = 5;

        public float[] Unknown1 { get; private set; }
        public float[] Unknown2 { get; private set; }
        public float[] Unknown3 { get; private set; }
        public float[] Unknown4 { get; private set; }
        public float[] Unknown5 { get; private set; }
        
        public DmaAddress[] Filler0 { get; private set; }

        public float[] Unknown6 { get; private set; }
        public float[] Unknown7 { get; private set; }
        public float[] Unknown8 { get; private set; }
        public float[] Unknown9 { get; private set; }
        public float[] Unknown10 { get; private set; }
        
        public DmaAddress[] Filler1 { get; private set; }

        public float[] Unknown11 { get; private set; }
        public float[] Unknown12 { get; private set; }
        public float[] Unknown13 { get; private set; }
        public float[] Unknown14 { get; private set; }
        public float[] Unknown15 { get; private set; }
        
        public DmaAddress[] Filler2 { get; private set; }

        public float[] KartSpeed50CC { get; private set; }
        public float[] KartSpeed100CC { get; private set; }
        public float[] KartSpeed150CC { get; private set; }
        public float[] KartSpeedExtra { get; private set; }
        public float[] KartSpeedBattle { get; private set; }
        
        public DmaAddress[] Filler3 { get; private set; }

        public float[] KartFriction { get; private set; }
        public float[] KartGravity { get; private set; }
        public float[] Unknown16 { get; private set; }
        public float[] KartTopSpeed { get; private set; }
        public float[] KartBoundingBox { get; private set; }

        public float[] Kart1UnknownBlock1 { get; private set; }
        public float[] Kart2UnknownBlock1 { get; private set; }
        public float[] Kart3UnknownBlock1 { get; private set; }
        public float[] Kart4UnknownBlock1 { get; private set; }
        public float[] Kart5UnknownBlock1 { get; private set; }
        public float[] Kart6UnknownBlock1 { get; private set; }
        public float[] Kart7UnknownBlock1 { get; private set; }
        public float[] Kart8UnknownBlock1 { get; private set; }

        public float[] Kart1UnknownBlock2 { get; private set; }
        public float[] Kart2UnknownBlock2 { get; private set; }
        public float[] Kart3UnknownBlock2 { get; private set; }
        public float[] Kart4UnknownBlock2 { get; private set; }
        public float[] Kart5UnknownBlock2 { get; private set; }
        public float[] Kart6UnknownBlock2 { get; private set; }
        public float[] Kart7UnknownBlock2 { get; private set; }
        public float[] Kart8UnknownBlock2 { get; private set; }
        
        public DmaAddress[] Filler4 { get; private set; }
        public DmaAddress[] Filler5 { get; private set; }

        public float[] Kart1UnknownBlock3 { get; private set; }
        public float[] Kart2UnknownBlock3 { get; private set; }
        public float[] Kart3UnknownBlock3 { get; private set; }
        public float[] Kart4UnknownBlock3 { get; private set; }
        public float[] Kart5UnknownBlock3 { get; private set; }
        public float[] Kart6UnknownBlock3 { get; private set; }
        public float[] Kart7UnknownBlock3 { get; private set; }
        public float[] Kart8UnknownBlock3 { get; private set; }

        public float[] Kart1UnknownBlock4 { get; private set; }
        public float[] Kart2UnknownBlock4 { get; private set; }
        public float[] Kart3UnknownBlock4 { get; private set; }
        public float[] Kart4UnknownBlock4 { get; private set; }
        public float[] Kart5UnknownBlock4 { get; private set; }
        public float[] Kart6UnknownBlock4 { get; private set; }
        public float[] Kart7UnknownBlock4 { get; private set; }
        public float[] Kart8UnknownBlock4 { get; private set; }
        
        public DmaAddress[] Filler6 { get; private set; }
        public DmaAddress[] Filler7 { get; private set; }

        public float[] Kart1Acceleration { get; private set; }
        public float[] Kart2Acceleration { get; private set; }
        public float[] Kart3Acceleration { get; private set; }
        public float[] Kart4Acceleration { get; private set; }
        public float[] Kart5Acceleration { get; private set; }
        public float[] Kart6Acceleration { get; private set; }
        public float[] Kart7Acceleration { get; private set; }
        public float[] Kart8Acceleration { get; private set; }
        
        public DmaAddress[] Filler8 { get; private set; }

        public float[] Kart1UnknownBlock5 { get; private set; }
        public float[] Kart2UnknownBlock5 { get; private set; }
        public float[] Kart3UnknownBlock5 { get; private set; }
        public float[] Kart4UnknownBlock5 { get; private set; }
        public float[] Kart5UnknownBlock5 { get; private set; }
        public float[] Kart6UnknownBlock5 { get; private set; }
        public float[] Kart7UnknownBlock5 { get; private set; }
        public float[] Kart8UnknownBlock5 { get; private set; }
        
        public DmaAddress[] Filler9 { get; private set; }

        public float[] Kart1UnknownBlock6 { get; private set; }
        public float[] Kart2UnknownBlock6 { get; private set; }
        public float[] Kart3UnknownBlock6 { get; private set; }
        public float[] Kart4UnknownBlock6 { get; private set; }
        public float[] Kart5UnknownBlock6 { get; private set; }
        public float[] Kart6UnknownBlock6 { get; private set; }
        public float[] Kart7UnknownBlock6 { get; private set; }
        public float[] Kart8UnknownBlock6 { get; private set; }
        
        public DmaAddress[] Filler10 { get; private set; }

        public float[] Kart1UnknownBlock7 { get; private set; }
        public float[] Kart2UnknownBlock7 { get; private set; }
        public float[] Kart3UnknownBlock7 { get; private set; }
        public float[] Kart4UnknownBlock7 { get; private set; }
        public float[] Kart5UnknownBlock7 { get; private set; }
        public float[] Kart6UnknownBlock7 { get; private set; }
        public float[] Kart7UnknownBlock7 { get; private set; }
        public float[] Kart8UnknownBlock7 { get; private set; }
        
        public DmaAddress[] Filler11 { get; private set; }

        public float[] KartHandling { get; private set; }
        public float[] Unknown17 { get; private set; }
        public float[] KartTurnSpeedReductionCoefficient { get; private set; }
        public float[] KartTurnSpeedReductionCoefficient2 { get; private set; }
        public float[] Unknown18 { get; private set; }
        public float[] KartHopHeight { get; private set; }
        public float[] KartHopFallSpeed { get; private set; }
        public float[] Unknown19 { get; private set; }
        public float[] Unknown20 { get; private set; }
        public float[] Unknown21 { get; private set; }
        public float[] Unknown22 { get; private set; }
        public float[] Unknown23 { get; private set; }
        public float[] Unknown24 { get; private set; }
        public float[] Unknown25 { get; private set; }
        public float[] Unknown26 { get; private set; }
        
        public KartInformationBlock(int offset, byte[] data)
            : base(offset, data)
        {

        }

        public KartInformationBlock(XElement xml, byte[] fileData)
            : base(xml, fileData)
        {

        }

        public void InitDataContainers()
        {
            if (Unknown1 == null) //Assume it's all null or all filled
            {
                Unknown1 = new float[CHARACTER_COUNT];
                Unknown2 = new float[CHARACTER_COUNT];
                Unknown3 = new float[CHARACTER_COUNT];
                Unknown4 = new float[CHARACTER_COUNT];
                Unknown5 = new float[CHARACTER_COUNT];

                Filler0 = new DmaAddress[FILLER_5_SIZE];

                Unknown6 = new float[CHARACTER_COUNT];
                Unknown7 = new float[CHARACTER_COUNT];
                Unknown8 = new float[CHARACTER_COUNT];
                Unknown9 = new float[CHARACTER_COUNT];
                Unknown10 = new float[CHARACTER_COUNT];
                
                Filler1 = new DmaAddress[FILLER_5_SIZE];

                Unknown11 = new float[CHARACTER_COUNT];
                Unknown12 = new float[CHARACTER_COUNT];
                Unknown13 = new float[CHARACTER_COUNT];
                Unknown14 = new float[CHARACTER_COUNT];
                Unknown15 = new float[CHARACTER_COUNT];
                
                Filler2 = new DmaAddress[FILLER_5_SIZE];
                
                KartSpeed50CC = new float[CHARACTER_COUNT];
                KartSpeed100CC = new float[CHARACTER_COUNT];
                KartSpeed150CC = new float[CHARACTER_COUNT];
                KartSpeedExtra = new float[CHARACTER_COUNT];
                KartSpeedBattle = new float[CHARACTER_COUNT];

                Filler3 = new DmaAddress[FILLER_5_SIZE];
                
                KartFriction = new float[CHARACTER_COUNT];
                KartGravity = new float[CHARACTER_COUNT];
                Unknown16 = new float[CHARACTER_COUNT];
                KartTopSpeed = new float[CHARACTER_COUNT];
                KartBoundingBox = new float[CHARACTER_COUNT];

                Kart1UnknownBlock1 = new float[UNKNOWN_BLOCK_SIZE];
                Kart2UnknownBlock1 = new float[UNKNOWN_BLOCK_SIZE];
                Kart3UnknownBlock1 = new float[UNKNOWN_BLOCK_SIZE];
                Kart4UnknownBlock1 = new float[UNKNOWN_BLOCK_SIZE];
                Kart5UnknownBlock1 = new float[UNKNOWN_BLOCK_SIZE];
                Kart6UnknownBlock1 = new float[UNKNOWN_BLOCK_SIZE];
                Kart7UnknownBlock1 = new float[UNKNOWN_BLOCK_SIZE];
                Kart8UnknownBlock1 = new float[UNKNOWN_BLOCK_SIZE];

                Kart1UnknownBlock2 = new float[UNKNOWN_BLOCK_SIZE];
                Kart2UnknownBlock2 = new float[UNKNOWN_BLOCK_SIZE];
                Kart3UnknownBlock2 = new float[UNKNOWN_BLOCK_SIZE];
                Kart4UnknownBlock2 = new float[UNKNOWN_BLOCK_SIZE];
                Kart5UnknownBlock2 = new float[UNKNOWN_BLOCK_SIZE];
                Kart6UnknownBlock2 = new float[UNKNOWN_BLOCK_SIZE];
                Kart7UnknownBlock2 = new float[UNKNOWN_BLOCK_SIZE];
                Kart8UnknownBlock2 = new float[UNKNOWN_BLOCK_SIZE];
                
                Filler4 = new DmaAddress[CHARACTER_COUNT];
                Filler5 = new DmaAddress[CHARACTER_COUNT];
                
                Kart1UnknownBlock3 = new float[UNKNOWN_BLOCK_SIZE];
                Kart2UnknownBlock3 = new float[UNKNOWN_BLOCK_SIZE];
                Kart3UnknownBlock3 = new float[UNKNOWN_BLOCK_SIZE];
                Kart4UnknownBlock3 = new float[UNKNOWN_BLOCK_SIZE];
                Kart5UnknownBlock3 = new float[UNKNOWN_BLOCK_SIZE];
                Kart6UnknownBlock3 = new float[UNKNOWN_BLOCK_SIZE];
                Kart7UnknownBlock3 = new float[UNKNOWN_BLOCK_SIZE];
                Kart8UnknownBlock3 = new float[UNKNOWN_BLOCK_SIZE];

                Kart1UnknownBlock4 = new float[UNKNOWN_BLOCK_SIZE];
                Kart2UnknownBlock4 = new float[UNKNOWN_BLOCK_SIZE];
                Kart3UnknownBlock4 = new float[UNKNOWN_BLOCK_SIZE];
                Kart4UnknownBlock4 = new float[UNKNOWN_BLOCK_SIZE];
                Kart5UnknownBlock4 = new float[UNKNOWN_BLOCK_SIZE];
                Kart6UnknownBlock4 = new float[UNKNOWN_BLOCK_SIZE];
                Kart7UnknownBlock4 = new float[UNKNOWN_BLOCK_SIZE];
                Kart8UnknownBlock4 = new float[UNKNOWN_BLOCK_SIZE];

                Filler6 = new DmaAddress[CHARACTER_COUNT];
                Filler7 = new DmaAddress[CHARACTER_COUNT];

                Kart1Acceleration = new float[ACCEL_BLOCK_SIZE];
                Kart2Acceleration = new float[ACCEL_BLOCK_SIZE];
                Kart3Acceleration = new float[ACCEL_BLOCK_SIZE];
                Kart4Acceleration = new float[ACCEL_BLOCK_SIZE];
                Kart5Acceleration = new float[ACCEL_BLOCK_SIZE];
                Kart6Acceleration = new float[ACCEL_BLOCK_SIZE];
                Kart7Acceleration = new float[ACCEL_BLOCK_SIZE];
                Kart8Acceleration = new float[ACCEL_BLOCK_SIZE];

                Filler8 = new DmaAddress[CHARACTER_COUNT];
                
                Kart1UnknownBlock5 = new float[UNKNOWN_BLOCK_SIZE];
                Kart2UnknownBlock5 = new float[UNKNOWN_BLOCK_SIZE];
                Kart3UnknownBlock5 = new float[UNKNOWN_BLOCK_SIZE];
                Kart4UnknownBlock5 = new float[UNKNOWN_BLOCK_SIZE];
                Kart5UnknownBlock5 = new float[UNKNOWN_BLOCK_SIZE];
                Kart6UnknownBlock5 = new float[UNKNOWN_BLOCK_SIZE];
                Kart7UnknownBlock5 = new float[UNKNOWN_BLOCK_SIZE];
                Kart8UnknownBlock5 = new float[UNKNOWN_BLOCK_SIZE];

                Filler9 = new DmaAddress[CHARACTER_COUNT];
                
                Kart1UnknownBlock6 = new float[UNKNOWN_BLOCK_SIZE];
                Kart2UnknownBlock6 = new float[UNKNOWN_BLOCK_SIZE];
                Kart3UnknownBlock6 = new float[UNKNOWN_BLOCK_SIZE];
                Kart4UnknownBlock6 = new float[UNKNOWN_BLOCK_SIZE];
                Kart5UnknownBlock6 = new float[UNKNOWN_BLOCK_SIZE];
                Kart6UnknownBlock6 = new float[UNKNOWN_BLOCK_SIZE];
                Kart7UnknownBlock6 = new float[UNKNOWN_BLOCK_SIZE];
                Kart8UnknownBlock6 = new float[UNKNOWN_BLOCK_SIZE];

                Filler10 = new DmaAddress[CHARACTER_COUNT];
                
                Kart1UnknownBlock7 = new float[UNKNOWN_BLOCK_SIZE];
                Kart2UnknownBlock7 = new float[UNKNOWN_BLOCK_SIZE];
                Kart3UnknownBlock7 = new float[UNKNOWN_BLOCK_SIZE];
                Kart4UnknownBlock7 = new float[UNKNOWN_BLOCK_SIZE];
                Kart5UnknownBlock7 = new float[UNKNOWN_BLOCK_SIZE];
                Kart6UnknownBlock7 = new float[UNKNOWN_BLOCK_SIZE];
                Kart7UnknownBlock7 = new float[UNKNOWN_BLOCK_SIZE];
                Kart8UnknownBlock7 = new float[UNKNOWN_BLOCK_SIZE];

                Filler11 = new DmaAddress[CHARACTER_COUNT];

                KartHandling = new float[CHARACTER_COUNT];
                Unknown17 = new float[CHARACTER_COUNT];
                KartTurnSpeedReductionCoefficient = new float[CHARACTER_COUNT];
                KartTurnSpeedReductionCoefficient2 = new float[CHARACTER_COUNT];
                Unknown18 = new float[CHARACTER_COUNT];
                KartHopHeight = new float[CHARACTER_COUNT];
                KartHopFallSpeed = new float[CHARACTER_COUNT];
                Unknown19 = new float[CHARACTER_COUNT];
                Unknown20 = new float[CHARACTER_COUNT];
                Unknown21 = new float[CHARACTER_COUNT];
                Unknown22 = new float[CHARACTER_COUNT];
                Unknown23 = new float[CHARACTER_COUNT];
                Unknown24 = new float[CHARACTER_COUNT];
                Unknown25 = new float[CHARACTER_COUNT];
                Unknown26 = new float[CHARACTER_COUNT];

            }
        }

        public override byte[] RawData
        {
            get
            {
                //READ IN ORDER!

                return ByteHelper.CombineIntoBytes(
                    //Unknown 1-5
                    GetArrayValuesForChars(Unknown1),
                    GetArrayValuesForChars(Unknown2),
                    GetArrayValuesForChars(Unknown3),
                    GetArrayValuesForChars(Unknown4),
                    GetArrayValuesForChars(Unknown5),
                    //Filler 0
                    Filler0,
                    //Uknknown 6-10
                    GetArrayValuesForChars(Unknown6),
                    GetArrayValuesForChars(Unknown7),
                    GetArrayValuesForChars(Unknown8),
                    GetArrayValuesForChars(Unknown9),
                    GetArrayValuesForChars(Unknown10),
                    //Filler 1
                    Filler1,
                    //Unknown 11-15
                    GetArrayValuesForChars(Unknown11),
                    GetArrayValuesForChars(Unknown12),
                    GetArrayValuesForChars(Unknown13),
                    GetArrayValuesForChars(Unknown14),
                    GetArrayValuesForChars(Unknown15),
                    //Filler 2
                    Filler2,
                    //KartSpeed__CCs
                    GetArrayValuesForChars(KartSpeed50CC),
                    GetArrayValuesForChars(KartSpeed100CC),
                    GetArrayValuesForChars(KartSpeed150CC),
                    GetArrayValuesForChars(KartSpeedExtra),
                    GetArrayValuesForChars(KartSpeedBattle),
                    //Filler 3
                    Filler3,
                    //Friction/Gravity
                    GetArrayValuesForChars(KartFriction),
                    GetArrayValuesForChars(KartGravity),
                    //Unknown16
                    GetArrayValuesForChars(Unknown16),
                    //TopSpeed/BoundingBox
                    GetArrayValuesForChars(KartTopSpeed),
                    GetArrayValuesForChars(KartBoundingBox),

                    //Block1/2
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 1),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 1),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 1),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 1),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 1),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 1),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 1),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 1),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 2),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 2),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 2),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 2),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 2),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 2),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 2),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 2),
                    //Filler4/5
                    Filler4,
                    Filler5,
                    //Block3/4
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 3),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 3),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 3),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 3),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 3),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 3),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 3),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 3),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 4),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 4),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 4),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 4),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 4),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 4),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 4),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 4),
                    //Filler6/7
                    Filler6,
                    Filler7,
                    //AccelBlock
                    getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario),
                    getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi),
                    getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi),
                    getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad),
                    getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK),
                    getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario),
                    getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach),
                    getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser),
                    //Filler8
                    Filler8,
                    //Block5
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 5),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 5),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 5),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 5),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 5),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 5),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 5),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 5),
                    //Filler9
                    Filler9,
                    //Block6
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 6),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 6),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 6),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 6),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 6),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 6),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 6),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 6),
                    //Filler10
                    Filler10,
                    //Block7
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 7),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 7),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 7),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 7),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 7),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 7),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 7),
                    GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 7),
                    //Filler11
                    Filler11,
                    //Handling
                    GetArrayValuesForChars(KartHandling),
                    //Unknown17
                    GetArrayValuesForChars(Unknown17),
                    //TurnSpeedRedux[1/2]
                    GetArrayValuesForChars(KartTurnSpeedReductionCoefficient),
                    GetArrayValuesForChars(KartTurnSpeedReductionCoefficient2),
                    //Unknown18
                    GetArrayValuesForChars(Unknown18),
                    //HopHeight/FallSpeed
                    GetArrayValuesForChars(KartHopHeight),
                    GetArrayValuesForChars(KartHopFallSpeed),
                    //Unknown19-26
                    GetArrayValuesForChars(Unknown19),
                    GetArrayValuesForChars(Unknown20),
                    GetArrayValuesForChars(Unknown21),
                    GetArrayValuesForChars(Unknown22),
                    GetArrayValuesForChars(Unknown23),
                    GetArrayValuesForChars(Unknown24),
                    GetArrayValuesForChars(Unknown25),
                    GetArrayValuesForChars(Unknown26)
                );
            }
            set
            {
                if (value.Length != RawDataSize)
                    return; //this is important

                InitDataContainers();

                //READ IN ORDER!
                int offset = 0;

                //Unknown 1-5
                SetArrayValuesForChars(value, offset, Unknown1);
                offset += Unknown1.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown2);
                offset += Unknown1.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown3);
                offset += Unknown1.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown4);
                offset += Unknown1.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown5);
                offset += Unknown1.Length * 4;

                //Filler 0
                SetFillerValues(value, offset, Filler0, FILLER_5_SIZE);
                offset += FILLER_5_SIZE * 4;

                //Uknknown 6-10
                SetArrayValuesForChars(value, offset, Unknown6);
                offset += Unknown6.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown7);
                offset += Unknown7.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown8);
                offset += Unknown8.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown9);
                offset += Unknown9.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown10);
                offset += Unknown10.Length * 4;

                //Filler 1
                SetFillerValues(value, offset, Filler1, FILLER_5_SIZE);
                offset += FILLER_5_SIZE * 4;

                //Unknown 11-15
                SetArrayValuesForChars(value, offset, Unknown11);
                offset += Unknown11.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown12);
                offset += Unknown12.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown13);
                offset += Unknown13.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown14);
                offset += Unknown14.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown15);
                offset += Unknown15.Length * 4;

                //Filler 2
                SetFillerValues(value, offset, Filler2, FILLER_5_SIZE);
                offset += FILLER_5_SIZE * 4;

                //KartSpeed__CCs
                SetArrayValuesForChars(value, offset, KartSpeed50CC);
                offset += KartSpeed50CC.Length * 4;
                SetArrayValuesForChars(value, offset, KartSpeed100CC);
                offset += KartSpeed100CC.Length * 4;
                SetArrayValuesForChars(value, offset, KartSpeed150CC);
                offset += KartSpeed150CC.Length * 4;
                SetArrayValuesForChars(value, offset, KartSpeedExtra);
                offset += KartSpeedExtra.Length * 4;
                SetArrayValuesForChars(value, offset, KartSpeedBattle);
                offset += KartSpeedBattle.Length * 4;

                //Filler 3
                SetFillerValues(value, offset, Filler3, FILLER_5_SIZE);
                offset += FILLER_5_SIZE * 4;

                //Friction/Gravity/Unknown16/TopSpeed/BoundingBox
                SetArrayValuesForChars(value, offset, KartFriction);
                offset += KartFriction.Length * 4;
                SetArrayValuesForChars(value, offset, KartGravity);
                offset += KartGravity.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown16);
                offset += Unknown16.Length * 4;
                SetArrayValuesForChars(value, offset, KartTopSpeed);
                offset += KartTopSpeed.Length * 4;
                SetArrayValuesForChars(value, offset, KartBoundingBox);
                offset += KartBoundingBox.Length * 4;

                //Block1/2
                float[] dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 1);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 1);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 1);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 1);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 1);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 1);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 1);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 1);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 2);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 2);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 2);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 2);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 2);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 2);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 2);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 2);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                //Filler4/5
                SetFillerValues(value, offset, Filler4, CHARACTER_COUNT);
                offset += CHARACTER_COUNT * 4;
                SetFillerValues(value, offset, Filler5, CHARACTER_COUNT);
                offset += CHARACTER_COUNT * 4;

                //Block3/4
                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 3);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 3);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 3);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 3);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 3);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 3);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 3);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 3);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 4);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 4);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 4);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 4);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 4);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 4);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 4);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 4);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;
                
                //Filler6/7
                SetFillerValues(value, offset, Filler6, CHARACTER_COUNT);
                offset += CHARACTER_COUNT * 4;
                SetFillerValues(value, offset, Filler7, CHARACTER_COUNT);
                offset += CHARACTER_COUNT * 4;

                //AccelBlock
                dataBlock = getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario);
                SetArrayValues(value, offset, dataBlock, ACCEL_BLOCK_SIZE);
                offset += ACCEL_BLOCK_SIZE * 4;

                dataBlock = getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi);
                SetArrayValues(value, offset, dataBlock, ACCEL_BLOCK_SIZE);
                offset += ACCEL_BLOCK_SIZE * 4;

                dataBlock = getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi);
                SetArrayValues(value, offset, dataBlock, ACCEL_BLOCK_SIZE);
                offset += ACCEL_BLOCK_SIZE * 4;

                dataBlock = getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad);
                SetArrayValues(value, offset, dataBlock, ACCEL_BLOCK_SIZE);
                offset += ACCEL_BLOCK_SIZE * 4;

                dataBlock = getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK);
                SetArrayValues(value, offset, dataBlock, ACCEL_BLOCK_SIZE);
                offset += ACCEL_BLOCK_SIZE * 4;

                dataBlock = getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario);
                SetArrayValues(value, offset, dataBlock, ACCEL_BLOCK_SIZE);
                offset += ACCEL_BLOCK_SIZE * 4;

                dataBlock = getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach);
                SetArrayValues(value, offset, dataBlock, ACCEL_BLOCK_SIZE);
                offset += ACCEL_BLOCK_SIZE * 4;

                dataBlock = getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser);
                SetArrayValues(value, offset, dataBlock, ACCEL_BLOCK_SIZE);
                offset += ACCEL_BLOCK_SIZE * 4;

                //Filler8
                SetFillerValues(value, offset, Filler8, CHARACTER_COUNT);
                offset += CHARACTER_COUNT * 4;

                //Block5
                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 5);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 5);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 5);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 5);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 5);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 5);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 5);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 5);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                //Filler9
                SetFillerValues(value, offset, Filler9, CHARACTER_COUNT);
                offset += CHARACTER_COUNT * 4;

                //Block6
                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 6);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 6);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 6);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 6);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 6);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 6);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 6);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 6);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                //Filler10
                SetFillerValues(value, offset, Filler10, CHARACTER_COUNT);
                offset += CHARACTER_COUNT * 4;

                //Block7
                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Mario, 7);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Luigi, 7);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Yoshi, 7);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Toad, 7);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.DK, 7);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Wario, 7);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Peach, 7);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                dataBlock = GetDataBlockFor(MarioKartRomInfo.OriginalCharacters.Bowser, 7);
                SetArrayValues(value, offset, dataBlock, UNKNOWN_BLOCK_SIZE);
                offset += UNKNOWN_BLOCK_SIZE * 4;

                //Filler11
                SetFillerValues(value, offset, Filler11, CHARACTER_COUNT);
                offset += CHARACTER_COUNT * 4;

                //Handling
                SetArrayValuesForChars(value, offset, KartHandling);
                offset += KartHandling.Length * 4;

                //Unknown17
                SetArrayValuesForChars(value, offset, Unknown17);
                offset += Unknown17.Length * 4;

                //TurnSpeedRedux[1/2]
                SetArrayValuesForChars(value, offset, KartTurnSpeedReductionCoefficient);
                offset += KartTurnSpeedReductionCoefficient.Length * 4;
                SetArrayValuesForChars(value, offset, KartTurnSpeedReductionCoefficient2);
                offset += KartTurnSpeedReductionCoefficient2.Length * 4;

                //Unknown18
                SetArrayValuesForChars(value, offset, Unknown18);
                offset += Unknown18.Length * 4;

                //HopHeight/FallSpeed
                SetArrayValuesForChars(value, offset, KartHopHeight);
                offset += KartHopHeight.Length * 4;
                SetArrayValuesForChars(value, offset, KartHopFallSpeed);
                offset += KartHopFallSpeed.Length * 4;

                //Unknown19-26
                SetArrayValuesForChars(value, offset, Unknown19);
                offset += Unknown19.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown20);
                offset += Unknown20.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown21);
                offset += Unknown21.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown22);
                offset += Unknown22.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown23);
                offset += Unknown23.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown24);
                offset += Unknown24.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown25);
                offset += Unknown25.Length * 4;
                SetArrayValuesForChars(value, offset, Unknown26);
                offset += Unknown26.Length * 4;
            }
        }
        
        public override int RawDataSize
        {
            get { return DefaultKartInformationBlock0End - DefaultKartInformationBlock0Location; }
        }

        private float[] getAccelDataBlockFor(MarioKartRomInfo.OriginalCharacters selectedChar)
        {
            int charNum = (int)selectedChar;
            switch (charNum)
            {
                case 0:
                    return Kart1Acceleration;
                case 1:
                    return Kart2Acceleration;
                case 2:
                    return Kart3Acceleration;
                case 3:
                    return Kart4Acceleration;
                case 4:
                    return Kart5Acceleration;
                case 5:
                    return Kart6Acceleration;
                case 6:
                    return Kart7Acceleration;
                case 7:
                    return Kart8Acceleration;

            }
            return null;
        }

        private float[] GetDataBlockFor(MarioKartRomInfo.OriginalCharacters selectedChar, int blockNum)
        {
            int charNum = (int)selectedChar;
            switch (charNum)
            {
                case 0:
                    switch (blockNum)
                    {
                        case 1:
                            return Kart1UnknownBlock1;
                        case 2:
                            return Kart1UnknownBlock2;
                        case 3:
                            return Kart1UnknownBlock3;
                        case 4:
                            return Kart1UnknownBlock4;
                        case 5:
                            return Kart1UnknownBlock5;
                        case 6:
                            return Kart1UnknownBlock6;
                        case 7:
                            return Kart1UnknownBlock7;
                    }
                    break;
                case 1:
                    switch (blockNum)
                    {
                        case 1:
                            return Kart2UnknownBlock1;
                        case 2:
                            return Kart2UnknownBlock2;
                        case 3:
                            return Kart2UnknownBlock3;
                        case 4:
                            return Kart2UnknownBlock4;
                        case 5:
                            return Kart2UnknownBlock5;
                        case 6:
                            return Kart2UnknownBlock6;
                        case 7:
                            return Kart2UnknownBlock7;
                    }
                    break;
                case 2:
                    switch (blockNum)
                    {
                        case 1:
                            return Kart3UnknownBlock1;
                        case 2:
                            return Kart3UnknownBlock2;
                        case 3:
                            return Kart3UnknownBlock3;
                        case 4:
                            return Kart3UnknownBlock4;
                        case 5:
                            return Kart3UnknownBlock5;
                        case 6:
                            return Kart3UnknownBlock6;
                        case 7:
                            return Kart3UnknownBlock7;
                    }
                    break;
                case 3:
                    switch (blockNum)
                    {
                        case 1:
                            return Kart4UnknownBlock1;
                        case 2:
                            return Kart4UnknownBlock2;
                        case 3:
                            return Kart4UnknownBlock3;
                        case 4:
                            return Kart4UnknownBlock4;
                        case 5:
                            return Kart4UnknownBlock5;
                        case 6:
                            return Kart4UnknownBlock6;
                        case 7:
                            return Kart4UnknownBlock7;
                    }
                    break;
                case 4:
                    switch (blockNum)
                    {
                        case 1:
                            return Kart5UnknownBlock1;
                        case 2:
                            return Kart5UnknownBlock2;
                        case 3:
                            return Kart5UnknownBlock3;
                        case 4:
                            return Kart5UnknownBlock4;
                        case 5:
                            return Kart5UnknownBlock5;
                        case 6:
                            return Kart5UnknownBlock6;
                        case 7:
                            return Kart5UnknownBlock7;
                    }
                    break;
                case 5:
                    switch (blockNum)
                    {
                        case 1:
                            return Kart6UnknownBlock1;
                        case 2:
                            return Kart6UnknownBlock2;
                        case 3:
                            return Kart6UnknownBlock3;
                        case 4:
                            return Kart6UnknownBlock4;
                        case 5:
                            return Kart6UnknownBlock5;
                        case 6:
                            return Kart6UnknownBlock6;
                        case 7:
                            return Kart6UnknownBlock7;
                    }
                    break;
                case 6:
                    switch (blockNum)
                    {
                        case 1:
                            return Kart7UnknownBlock1;
                        case 2:
                            return Kart7UnknownBlock2;
                        case 3:
                            return Kart7UnknownBlock3;
                        case 4:
                            return Kart7UnknownBlock4;
                        case 5:
                            return Kart7UnknownBlock5;
                        case 6:
                            return Kart7UnknownBlock6;
                        case 7:
                            return Kart7UnknownBlock7;
                    }
                    break;
                case 7:
                    switch (blockNum)
                    {
                        case 1:
                            return Kart8UnknownBlock1;
                        case 2:
                            return Kart8UnknownBlock2;
                        case 3:
                            return Kart8UnknownBlock3;
                        case 4:
                            return Kart8UnknownBlock4;
                        case 5:
                            return Kart8UnknownBlock5;
                        case 6:
                            return Kart8UnknownBlock6;
                        case 7:
                            return Kart8UnknownBlock7;
                    }
                    break;
            }

            return null;
        }

        private void SetFillerValues(byte[] rawData, int offset, DmaAddress[] array, int elementCount)
        {
            for(int i = 0; i < elementCount; i++)
                array[i] = new DmaAddress(ByteHelper.ReadInt(rawData, offset + 4 * i));
        }

        private byte[] GetArrayValuesForChars(float[] array)
        {
            return ByteHelper.CombineIntoBytes(array[(int)MarioKartRomInfo.OriginalCharacters.Mario],
                array[(int)MarioKartRomInfo.OriginalCharacters.Luigi],
                array[(int)MarioKartRomInfo.OriginalCharacters.Yoshi],
                array[(int)MarioKartRomInfo.OriginalCharacters.Toad],
                array[(int)MarioKartRomInfo.OriginalCharacters.DK],
                array[(int)MarioKartRomInfo.OriginalCharacters.Wario],
                array[(int)MarioKartRomInfo.OriginalCharacters.Peach],
                array[(int)MarioKartRomInfo.OriginalCharacters.Bowser]);
        }

        private void SetArrayValues(byte[] rawData, int offset, float[] array, int elementCount)
        {
            for (int i = 0; i < elementCount; i++)
                array[i] = ByteHelper.ReadFloat(rawData, offset + 4 * i);
        }

        private void SetArrayValuesForChars(byte[] rawData, int offset, float[] array)
        {
            array[(int)MarioKartRomInfo.OriginalCharacters.Mario] = ByteHelper.ReadFloat(rawData, offset + 0x0);
            array[(int)MarioKartRomInfo.OriginalCharacters.Luigi] = ByteHelper.ReadFloat(rawData, offset + 0x4);
            array[(int)MarioKartRomInfo.OriginalCharacters.Yoshi] = ByteHelper.ReadFloat(rawData, offset + 0x8);
            array[(int)MarioKartRomInfo.OriginalCharacters.Toad] = ByteHelper.ReadFloat(rawData, offset + 0xC);
            array[(int)MarioKartRomInfo.OriginalCharacters.DK] = ByteHelper.ReadFloat(rawData, offset + 0x10);
            array[(int)MarioKartRomInfo.OriginalCharacters.Wario] = ByteHelper.ReadFloat(rawData, offset + 0x14);
            array[(int)MarioKartRomInfo.OriginalCharacters.Peach] = ByteHelper.ReadFloat(rawData, offset + 0x18);
            array[(int)MarioKartRomInfo.OriginalCharacters.Bowser] = ByteHelper.ReadFloat(rawData, offset + 0x1C);
        }

        public void SetStatsFromKart(int kartIndex, KartStats stats)
        {
            Unknown1[kartIndex] = stats.Unknown1;
            Unknown2[kartIndex] = stats.Unknown2;
            Unknown3[kartIndex] = stats.Unknown3;
            Unknown4[kartIndex] = stats.Unknown4;
            Unknown5[kartIndex] = stats.Unknown5;

            Unknown6[kartIndex] = stats.Unknown6;
            Unknown7[kartIndex] = stats.Unknown7;
            Unknown8[kartIndex] = stats.Unknown8;
            Unknown9[kartIndex] = stats.Unknown9;
            Unknown10[kartIndex] = stats.Unknown10;

            Unknown11[kartIndex] = stats.Unknown11;
            Unknown12[kartIndex] = stats.Unknown12;
            Unknown13[kartIndex] = stats.Unknown13;
            Unknown14[kartIndex] = stats.Unknown14;
            Unknown15[kartIndex] = stats.Unknown15;

            KartSpeed50CC[kartIndex] = stats.Speed50CC;
            KartSpeed100CC[kartIndex] = stats.Speed100CC;
            KartSpeed150CC[kartIndex] = stats.Speed150CC;
            KartSpeedExtra[kartIndex] = stats.SpeedExtra;
            KartSpeedBattle[kartIndex] = stats.SpeedBattle;

            KartFriction[kartIndex] = stats.Friction;
            KartGravity[kartIndex] = stats.Gravity;
            Unknown16[kartIndex] = stats.Unknown16;
            KartTopSpeed[kartIndex] = stats.TopSpeed;
            KartBoundingBox[kartIndex] = stats.BoundingBox;

            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 1),
                stats.Block1Unknowns, stats.Block1Unknowns.Length);
            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 2),
                stats.Block2Unknowns, stats.Block2Unknowns.Length);
            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 3),
                stats.Block3Unknowns, stats.Block3Unknowns.Length);
            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 4),
                stats.Block4Unknowns, stats.Block4Unknowns.Length);

            Array.Copy(getAccelDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex),
                stats.AccelBlock, stats.AccelBlock.Length);

            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 5),
                stats.Block5Unknowns, stats.Block5Unknowns.Length);
            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 6),
                stats.Block6Unknowns, stats.Block6Unknowns.Length);
            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 7),
                stats.Block7Unknowns, stats.Block7Unknowns.Length);

            KartHandling[kartIndex] = stats.Handling;
            Unknown17[kartIndex] = stats.Unknown17;
            KartTurnSpeedReductionCoefficient[kartIndex] = stats.TurnSpeedReductionCoefficient;
            KartTurnSpeedReductionCoefficient2[kartIndex] = stats.TurnSpeedReductionCoefficient2;
            Unknown18[kartIndex] = stats.Unknown18;
            KartHopHeight[kartIndex] = stats.HopHeight;
            KartHopFallSpeed[kartIndex] = stats.HopFallSpeed;
            Unknown19[kartIndex] = stats.Unknown19;
            Unknown20[kartIndex] = stats.Unknown20;
            Unknown21[kartIndex] = stats.Unknown21;
            Unknown22[kartIndex] = stats.Unknown22;
            Unknown23[kartIndex] = stats.Unknown23;
            Unknown24[kartIndex] = stats.Unknown24;
            Unknown25[kartIndex] = stats.Unknown25;
            Unknown26[kartIndex] = stats.Unknown26;
            
        }

        public KartStats GetKartStatsFor(int kartIndex)
        {
            KartStats stats = new KartStats();

            stats.Unknown1 = Unknown1[kartIndex];
            stats.Unknown2 = Unknown2[kartIndex];
            stats.Unknown3 = Unknown3[kartIndex];
            stats.Unknown4 = Unknown4[kartIndex];
            stats.Unknown5 = Unknown5[kartIndex];

            stats.Unknown6 = Unknown6[kartIndex];
            stats.Unknown7 = Unknown7[kartIndex];
            stats.Unknown8 = Unknown8[kartIndex];
            stats.Unknown9 = Unknown9[kartIndex];
            stats.Unknown10 = Unknown10[kartIndex];

            stats.Unknown11 = Unknown11[kartIndex];
            stats.Unknown12 = Unknown12[kartIndex];
            stats.Unknown13 = Unknown13[kartIndex];
            stats.Unknown14 = Unknown14[kartIndex];
            stats.Unknown15 = Unknown15[kartIndex];

            stats.Speed50CC = KartSpeed50CC[kartIndex];
            stats.Speed100CC = KartSpeed100CC[kartIndex];
            stats.Speed150CC = KartSpeed150CC[kartIndex];
            stats.SpeedExtra = KartSpeedExtra[kartIndex];
            stats.SpeedBattle = KartSpeedBattle[kartIndex];

            stats.Friction = KartFriction[kartIndex];
            stats.Gravity = KartGravity[kartIndex];
            stats.Unknown16 = Unknown16[kartIndex];
            stats.TopSpeed = KartTopSpeed[kartIndex];
            stats.BoundingBox = KartBoundingBox[kartIndex];

            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 1),
                stats.Block1Unknowns, stats.Block1Unknowns.Length);
            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 2),
                stats.Block2Unknowns, stats.Block2Unknowns.Length);
            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 3),
                stats.Block3Unknowns, stats.Block3Unknowns.Length);
            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 4),
                stats.Block4Unknowns, stats.Block4Unknowns.Length);

            Array.Copy(getAccelDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex),
                stats.AccelBlock, stats.AccelBlock.Length);

            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 5),
                stats.Block5Unknowns, stats.Block5Unknowns.Length);
            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 6),
                stats.Block6Unknowns, stats.Block6Unknowns.Length);
            Array.Copy(GetDataBlockFor((MarioKartRomInfo.OriginalCharacters)kartIndex, 7),
                stats.Block7Unknowns, stats.Block7Unknowns.Length);

            stats.Handling = KartHandling[kartIndex];
            stats.Unknown17 = Unknown17[kartIndex];
            stats.TurnSpeedReductionCoefficient = KartTurnSpeedReductionCoefficient[kartIndex];
            stats.TurnSpeedReductionCoefficient2 = KartTurnSpeedReductionCoefficient2[kartIndex];
            stats.Unknown18 = Unknown18[kartIndex];
            stats.HopHeight = KartHopHeight[kartIndex];
            stats.HopFallSpeed = KartHopFallSpeed[kartIndex];
            stats.Unknown19 = Unknown19[kartIndex];
            stats.Unknown20 = Unknown20[kartIndex];
            stats.Unknown21 = Unknown21[kartIndex];
            stats.Unknown22 = Unknown22[kartIndex];
            stats.Unknown23 = Unknown23[kartIndex];
            stats.Unknown24 = Unknown24[kartIndex];
            stats.Unknown25 = Unknown25[kartIndex];
            stats.Unknown26 = Unknown26[kartIndex];

            return stats;
        }
    }
}
