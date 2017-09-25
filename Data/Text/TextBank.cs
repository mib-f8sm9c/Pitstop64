using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Rom;

namespace Pitstop64.Data.Text
{
    public class TextBank
    {
        public enum TextType
        {
            //Cup text
            Cup_1_1 = 0,
            Cup_2_1,
            Cup_3_1,
            Cup_4_1,
            Battle_Message,
            Cup_1_2,
            Cup_2_2,
            Cup_3_2,
            Cup_4_2,

            //Track text
            Track_1_1,
            Track_2_1,
            Track_3_1,
            Track_4_1,
            Track_5_1,
            Track_6_1,
            Track_7_1,
            Track_8_1,
            Track_9_1,
            Track_10_1,
            Track_11_1,
            Track_12_1,
            Track_13_1,
            Track_14_1,
            Track_15_1,
            Track_16_1,
            Track_17_1,
            Track_18_1,
            Track_19_1,
            Track_20_1,

            Track_1_2,
            Track_2_2,
            Track_3_2,
            Track_4_2,
            Track_5_2,
            Track_6_2,
            Track_7_2,
            Track_8_2,
            Track_9_2,
            Track_10_2,
            Track_11_2,
            Track_12_2,
            Track_13_2,
            Track_14_2,
            Track_15_2,
            Track_16_2,
            Track_17_2,
            Track_18_2,
            Track_19_2,
            Track_20_2,

            Track_1_3,
            Track_2_3,
            Track_3_3,
            Track_4_3,
            Track_5_3,
            Track_6_3,
            Track_7_3,
            Track_8_3,
            Track_9_3,
            Track_10_3,
            Track_11_3,
            Track_12_3,
            Track_13_3,
            Track_14_3,
            Track_15_3,
            Track_16_3,
            Track_17_3,
            Track_18_3,
            Track_19_3,
            Track_20_3,

            Track_1_Short,
            Track_2_Short,
            Track_3_Short,
            Track_4_Short,
            Track_5_Short,
            Track_6_Short,
            Track_7_Short,
            Track_8_Short,
            Track_9_Short,
            Track_10_Short,
            Track_11_Short,
            Track_12_Short,
            Track_13_Short,
            Track_14_Short,
            Track_15_Short,
            Track_16_Short,
            Track_17_Short,
            Track_18_Short,
            Track_19_Short,
            Track_20_Short,

            //Medal types
            Place_None,
            Place_Bronze,
            Place_Silver,
            Place_Gold,

            //Karts
            Kart_1_1,
            Kart_2_1,
            Kart_5_1,
            Kart_4_1,
            Kart_6_1,
            Kart_7_1,
            Kart_3_1,
            Kart_8_1,
            Kart_1_2,
            Kart_2_2,
            Kart_5_2,
            Kart_4_2,
            Kart_6_2,
            Kart_7_2,
            Kart_3_2,
            Kart_8_2,

            //TBD
            Unknown_109,
            Unknown_110,
            Unknown_111,
            Unknown_112,
            Unknown_113,
            Unknown_114,
            Unknown_115,
            Unknown_116,
            Unknown_117,
            Unknown_118,
            Unknown_119,
            Unknown_120,
            Unknown_121,
            Unknown_122,


            Audio_Stereo,
            Audio_Headphone,
            Unknown_125,
            Audio_Mono,
            Audio_Stereo_Caps,
            Audio_Headphone_Caps,
            Unknown_129,
            Audio_Mono_Caps,
            Winner_Message,
            Loser_Message,
            Best_Records_Message,
            Best_Lap_Message,
            Lap_Time_Message,
            Lap_1_Message,
            Lap_2_Message,
            Lap_3_Message,


            Unknown_139,
            Unknown_140,
            Unknown_141,
            Unknown_142,
            Unknown_143,
            Unknown_144,
            Unknown_145,
            Unknown_146,
            Unknown_147,
            Unknown_148,
            Unknown_149,
            Unknown_150,
            Unknown_151,
            Unknown_152,
            Unknown_153,
            Unknown_154,
            Unknown_155,
            Unknown_156,
            Unknown_157,
            Unknown_158,
            Unknown_159,

            //Track info text
            Track_1_Length,
            Track_2_Length,
            Track_3_Length,
            Track_4_Length,
            Track_5_Length,
            Track_6_Length,
            Track_7_Length,
            Track_8_Length,
            Track_9_Length,
            Track_10_Length,
            Track_11_Length,
            Track_12_Length,
            Track_13_Length,
            Track_14_Length,
            Track_15_Length,
            Track_16_Length,
            Track_17_Length,
            Track_18_Length,
            Track_19_Length,
            Track_20_Length,

            Return_To_Menu_Message,
            Erase_Track_Records,
            Erase_Track_Ghost,

            Unknown_183,
            Unknown_184,

            Confirm_Erase_Track_Records_1,
            Confirm_Erase_Track_Records_2,
            Confirm_Erase_Track_Records_3,
            Confirm_Erase_Track_Ghost_1,
            Confirm_Erase_Track_Ghost_2,
            Confirm_Erase_Track_Ghost_3,


            Unknown_191,
            Unknown_192,
            Unknown_193,
            Unknown_194,
            Unknown_195,
            Unknown_196,
            Unknown_197,
            Unknown_198,
            Unknown_199,

            Unknown_200,
            Unknown_201,
            Unknown_202,
            Unknown_203,
            Unknown_204,
            Unknown_205,
            Unknown_206,
            Unknown_207,
            Unknown_208,
            Unknown_209,

            Unknown_210,
            Unknown_211,
            Unknown_212,
            Unknown_213,
            Unknown_214,
            Unknown_215,
            Unknown_216,
            Unknown_217,
            Unknown_218,
            Unknown_219,

            Unknown_220,
            Unknown_221,
            Unknown_222,
            Unknown_223,
            Unknown_224,
            Unknown_225,
            Unknown_226,
            Unknown_227,
            Unknown_228,
            Unknown_229,

            Unknown_230,
            Unknown_231,
            Unknown_232,
            Unknown_233,
            Unknown_234,
            Unknown_235,
            Unknown_236,
            Unknown_237,
            Unknown_238,
            Unknown_239,

            Unknown_240,
            Unknown_241,
            Unknown_242,
            Unknown_243,
            Unknown_244,
            Unknown_245,
            Unknown_246,
            Unknown_247,
            Unknown_248,
            Unknown_249,

            Unknown_250,
            Unknown_251,
            Unknown_252,
            Unknown_253,
            Unknown_254,
            Unknown_255,
            Unknown_256,
            Unknown_257,
            Unknown_258,
            Unknown_259,

            Unknown_260,
            Unknown_261,
            Unknown_262,
            Unknown_263,
            Unknown_264,
            Unknown_265,
            Unknown_266,
            Unknown_267,
            Unknown_268,
            Unknown_269,

            Unknown_270,
            Unknown_271,
            Unknown_272,
            Unknown_273,
            Unknown_274,
            Unknown_275,
            Unknown_276,
            Unknown_277,
            Unknown_278,
            Unknown_279,

            Unknown_280,
            Unknown_281,
            Unknown_282,
            Unknown_283,
            Unknown_284,
            Unknown_285,
            Unknown_286,
            Unknown_287,
            Unknown_288,
            Unknown_289,

            Unknown_290,
            Unknown_291,
            Unknown_292,
            Unknown_293,
            Unknown_294,
            Unknown_295,
            Unknown_296,
            Unknown_297,
            Unknown_298,
            Unknown_299,

            Unknown_300,
            Unknown_301,
            Unknown_302,
            Unknown_303,
            Unknown_304,
            Unknown_305,
            Unknown_306,
            Unknown_307,
            Unknown_308,
            Unknown_309,

            Unknown_310,
            Unknown_311,
            Unknown_312,
            Unknown_313,
            Unknown_314,
            Unknown_315,
            Unknown_316,
            Unknown_317,
            Unknown_318,
            Unknown_319,

            Unknown_320,
            Unknown_321,
            Unknown_322,
            Unknown_323,
            Unknown_324,
            Unknown_325,
            Unknown_326,
            Unknown_327,
            Unknown_328,
            Unknown_329,

            Unknown_330,
            Unknown_331,
            Unknown_332,
            Unknown_333,
            Unknown_334,
            Unknown_335,
            Unknown_336,

            First_Abbreviation,
            Second_Abbreviation,
            Third_Abbreviation,
            Fourth_Abbreviation,
            Fifth_Abbreviation,
            Sixth_Abbreviation,
            Seventh_Abbreviation,
            Eighth_Abbreviation
            //Here we'll label all the texts in order. Hopefully it doesn't switch between versions!!
        }

        public static int TEXT_DMA_REFERENCE_OFFSET = 0xC00;

        public TextBankBlock BankBlock { get; private set; }
        public TextReferenceBlock ReferenceBlock { get; private set; }

        public int FreeBankSpace { get { return BankBlock.FreeSpace; } }
        private List<string> _textValues; //Excluding the ending 0x00 character

        private int _accumulatedLengthChange; //Records how much the text length has changed. Resets on saving

        public TextBank(TextBankBlock bank, TextReferenceBlock references, bool optimize = false)
        {
            _textValues = new List<string>();
            BankBlock = bank;
            ReferenceBlock = references;

            ReadTextFromBlocks();

            if (optimize)
                SaveTextToBlocks();
        }

        public string GetText(TextType tType)
        {
            if (((int)tType) < _textValues.Count)
                return _textValues[(int)tType];

            return "ERROR";
        }

        public bool SetText(TextType tType, string value)
        {
            if (((int)tType) < _textValues.Count)
            {
                int currentStringLength = GetText(tType).Length;
                if (value.Length - currentStringLength + _accumulatedLengthChange > BankBlock.FreeSpace)
                    return false;

                _textValues[(int)tType] = value;
                _accumulatedLengthChange += value.Length - currentStringLength;

                return true;
            }

            return false;
        }

        private void ReadTextFromBlocks()
        {
            //Note: if the developers went crazy and had overlapping string references, this function
            //   will duplicate the strings instead. So be on the lookout for that
            _textValues.Clear();

            List<DmaAddress> allAddresses = new List<DmaAddress>();
            allAddresses.AddRange(ReferenceBlock.TextReferences1);
            allAddresses.AddRange(ReferenceBlock.TextReferences2);
            allAddresses.AddRange(ReferenceBlock.TextReferences3);
            allAddresses.AddRange(ReferenceBlock.TextReferences4);

            List<byte> stringBytes = new List<byte>(); 

            foreach (DmaAddress address in allAddresses)
            {
                int offset = (address.Offset + TEXT_DMA_REFERENCE_OFFSET) - TextBankBlock.TEXT_BLOCK_START;

                stringBytes.Clear();

                while (BankBlock.RawData[offset] != 0x00)
                {
                    stringBytes.Add(BankBlock.RawData[offset]);
                    offset++;

                    if (offset >= BankBlock.RawData.Length)
                        throw new Exception();
                }
                _textValues.Add(System.Text.Encoding.ASCII.GetString(stringBytes.ToArray()));
            }
        }

        public void SaveTextToBlocks()
        {
            int dmaOffset = TextBankBlock.TEXT_BLOCK_START - TEXT_DMA_REFERENCE_OFFSET;
            int offset = 0;
            byte[] newData = new byte[TextBankBlock.TEXT_BLOCK_LENGTH];
            byte[] strData;

            List<DmaAddress> allAddresses = new List<DmaAddress>();
            allAddresses.AddRange(ReferenceBlock.TextReferences1);
            allAddresses.AddRange(ReferenceBlock.TextReferences2);
            allAddresses.AddRange(ReferenceBlock.TextReferences3);
            allAddresses.AddRange(ReferenceBlock.TextReferences4);

            int startInterruptOffset = TextBankBlock.TEXT_INTERRUPT_START - TextBankBlock.TEXT_BLOCK_START;
            int endInterruptOffset = TextBankBlock.TEXT_INTERRUPT_END - TextBankBlock.TEXT_BLOCK_START;

            for (int i = 0; i < _textValues.Count; i++)
            {
                string str = _textValues[i];
                strData = System.Text.Encoding.ASCII.GetBytes(str);

                //Check to see if the string with overwrite the interrupt section, and skip past it if it does
                if ((startInterruptOffset <= offset && offset < endInterruptOffset) ||
                    (offset <= startInterruptOffset && startInterruptOffset < offset + strData.Length + 1) ||
                    (startInterruptOffset <= offset + strData.Length + 1 && offset + strData.Length + 1 < endInterruptOffset))
                {
                    offset = endInterruptOffset;
                }

                Array.Copy(strData, 0, newData, offset, strData.Length);
                newData[offset + strData.Length] = (byte)0x00;

                allAddresses[i].Offset = offset + dmaOffset;

                offset += strData.Length + 1;
            }

            BankBlock.RawData = newData;
        }
    }
}
