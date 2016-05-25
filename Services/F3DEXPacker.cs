using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Microcodes.F3DEX.DataElements.Commands;
using Cereal64.Common.Utils;
using Cereal64.Microcodes.F3DEX.DataElements;

//Source: https://github.com/RenaKunisaki/mariokart64/wiki/Compressed-Display-Lists

namespace MarioKartTestingTool
{
    public static class F3DEXPacker
    {
        private static List<uint> ImgTypes = new List<uint>() { 0, 0, 0, 3, 3, 3, 0 };
        private static List<uint> ImgFlag1s = new List<uint>() { 0x20, 0x20, 0x40, 0x20, 0x20, 0x40, 0x20 };
        private static List<uint> ImgFlag2s = new List<uint>() { 0x20, 0x40, 0x20, 0x20, 0x40, 0x20, 0x20 };

        public static List<F3DEXCommand> BytesToCommands(List<byte> bytes)
        {
            List<F3DEXCommand> commands = new List<F3DEXCommand>();

            int currentIndex = 0, imgIndex;

            uint tempUInt, outputUInt1, outputUInt2;
            byte commandByte, paramByte1, paramByte2, paramByte3;

            while (currentIndex < bytes.Count)
            {
                commandByte = bytes[currentIndex];
                switch (bytes[currentIndex])
                {
                    case 0x00:
                    case 0x01:
                    case 0x02:
                    case 0x03:
                    case 0x04:
                    case 0x05:
                    case 0x06:
                    case 0x07:
                    case 0x08:
                    case 0x09:
                    case 0x0A:
                    case 0x0B:
                    case 0x0C:
                    case 0x0D:
                    case 0x0E:
                    case 0x0F:
                    case 0x10:
                    case 0x11:
                    case 0x12:
                    case 0x13:
                    case 0x14:
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xBC, 0x00, 0x00, 0x02, 0x80, 0x00, 0x00, 0x40 }));
                        tempUInt = 0x09000000 | (uint)(commandByte * 0x18);
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes((byte)0x03, (byte)0x86, (byte)0x00, (byte)0x10, 
                                tempUInt)));
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes((byte)0x03, (byte)0x88, (byte)0x00, (byte)0x10, 
                                (tempUInt + 8))));
                        break;
                    case 0x15:
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xFC, 0x12, 0x18, 0x24, 0xFF, 0x33, 0xFF, 0xFF }));
                        break;
                    case 0x16:
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xFC, 0x12, 0x7E, 0x24, 0xFF, 0xFF, 0xF3, 0xF9 }));
                        break;
                    case 0x17:
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xFC, 0xFF, 0xFF, 0xFF, 0xFF, 0xFE, 0x79, 0x3C }));
                        break;
                    case 0x18:
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xB9, 0x00, 0x03, 0x1D, 0x00, 0x55, 0x20, 0x78 }));
                        break;
                    case 0x19:
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xB9, 0x00, 0x03, 0x1D, 0x00, 0x55, 0x30, 0x78 }));
                        break;
                    case 0x1A:
                    case 0x1B:
                    case 0x1C:
                    case 0x1D:
                    case 0x1E:
                    case 0x1F:
                    case 0x2C:
                        //Set Texture info
                        currentIndex++;
                        paramByte1 = bytes[currentIndex];
                        currentIndex++;
                        paramByte2 = bytes[currentIndex];

                        if (commandByte == 0x2C)
                        {
                            imgIndex = 6;
                            tempUInt = 0x100;
                        }
                        else
                        {
                            imgIndex = commandByte - 0x1A;
                            tempUInt = 0x000;
                        }

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xE8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }));

                        outputUInt1 = (((ImgTypes[imgIndex] << 0x15) | 0xF5100000) | ((((ImgFlag2s[imgIndex] << 1) + 7) >> 3) << 9)) | tempUInt;
                        outputUInt2 = (uint)(((((paramByte2 & 0xF) << 0x12) | (((paramByte2 & 0xF0) >> 4) << 0xE)) | ((paramByte1 & 0xF) << 8)) | (((paramByte1 & 0xF0) >> 4) << 4));

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes(outputUInt1, outputUInt2)));

                        outputUInt1 = 0xF2000000;
                        outputUInt2 = (((ImgFlag2s[imgIndex] - 1) << 0xE) | ((ImgFlag1s[imgIndex] - 1) << 2));

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes(outputUInt1, outputUInt2)));

                        break;
                    case 0x20:
                    case 0x21:
                    case 0x22:
                    case 0x23:
                    case 0x24:
                    case 0x25:
                        //Select Texture
                        currentIndex++;
                        paramByte1 = bytes[currentIndex];
                        currentIndex++;
                        paramByte2 = bytes[currentIndex];
                        currentIndex++;
                        paramByte3 = bytes[currentIndex];

                        imgIndex = commandByte - 0x20;

                        outputUInt1 = (ImgTypes[imgIndex] | 0xFD000000) | 0x100000; //SetTextureLoc
                        outputUInt2 = (uint)(paramByte1 << 0xB) + 0x05000000;

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes(outputUInt1, outputUInt2)));

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xE8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }));
                        
                        outputUInt1 = (((ImgTypes[imgIndex] << 0x15) | 0xF5000000) | 0x100000) | (uint)(paramByte3 & 0xF); //SetTile
                        outputUInt2 = (uint)(((paramByte3 & 0xF0) >> 4) << 0x18);

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes(outputUInt1, outputUInt2)));
                        
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xE6, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }));

                        uint imgSize = (ImgFlag2s[imgIndex] * ImgFlag1s[imgIndex]) - 1;

                        tempUInt = (ImgFlag2s[imgIndex] << 1) >> 3;
                        if (tempUInt == 0) tempUInt = 1;

                        outputUInt1 = 0xF3000000; //LoadBlock
                        outputUInt2 = (uint)(((tempUInt + 0x7FF) / tempUInt) | (uint)(((paramByte3 & 0xF0) >> 4) << 0x18)) | 
                            (imgSize << 0xC);

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes(outputUInt1, outputUInt2)));
                        
                        break;
                    case 0x26:
                        //Begin display list
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xBB, 0x00, 0x00, 0x01, 0xFF, 0xFF, 0xFF, 0xFF }));
                        break;
                    case 0x27:
                        //Begin display list alternate
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xBB, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01 }));
                        break;
                    case 0x28:
                        currentIndex++;
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0x04, 0x06, 0x81, 0xFF, 0x04, 0x05, 0x05, 0x00 }));
                        break;

                    case 0x29:
                        //DrawTriangle // REVIEW THAT THE V VARIABLES ARE CORRECT
                        currentIndex++;
                        paramByte1 = bytes[currentIndex];
                        currentIndex++;
                        paramByte2 = bytes[currentIndex];

                        ushort combinedParam = (ushort)((paramByte1 << 8) | paramByte2);
                        ushort v0 = (ushort)((combinedParam & 0x1F00) >> 8);
                        ushort v1 = (ushort)(((combinedParam & 0x0003) << 3) | ((combinedParam & 0xE000) >> 13));
                        ushort v2 = (ushort)((combinedParam & 0x007C) >> 2);


                        outputUInt1 = 0xBF000000;
                        outputUInt2 = (uint)((v2 << 17) | (v1 << 9) | (v0 << 1));

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes(outputUInt1, outputUInt2)));
                        break;
                    case 0x2A:
                        //End display list
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }));
                            break;
                    case 0x2B:
                        //Call list
                        currentIndex++;
                        paramByte1 = bytes[currentIndex];
                        currentIndex++;
                        paramByte2 = bytes[currentIndex];

                        uint combinedParams = (uint)((paramByte2 << 8) | paramByte1);

                        outputUInt1 = 0x06000000;
                        outputUInt2 = (uint)((0x07000000) | (combinedParams * 8));

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes(outputUInt1, outputUInt2)));
                        break;
                    case 0x2D:
                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xBE, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x40 }));
                        break;
                    case 0x2E: //no notes
                    case 0x2F: //no notes
                    case 0x30: //no notes
                    case 0x31: //no op
                    case 0x32: //no op

                        break;
                    case 0x33:
                    case 0x34:
                    case 0x35:
                    case 0x36:
                    case 0x37:
                    case 0x38:
                    case 0x39:
                    case 0x3A:
                    case 0x3B:
                    case 0x3C:
                    case 0x3D:
                    case 0x3E:
                    case 0x3F:
                    case 0x40:
                    case 0x41:
                    case 0x42:
                    case 0x43:
                    case 0x44:
                    case 0x45:
                    case 0x46:
                    case 0x47:
                    case 0x48:
                    case 0x49:
                    case 0x4A:
                    case 0x4B:
                    case 0x4C:
                    case 0x4D:
                    case 0x4E:
                    case 0x4F:
                    case 0x50:
                    case 0x51:
                    case 0x52:
                        currentIndex++;
                        paramByte1 = bytes[currentIndex];
                        currentIndex++;
                        paramByte2 = bytes[currentIndex];

                        uint combinedParamss = (uint)((paramByte2 << 8) | paramByte1);

                        outputUInt1 = (uint)(0x04000000 | (((commandByte - 0x32) * 0x410) - 1)); //LoadVtx
                        outputUInt2 = (uint)(0x04000000 | (combinedParamss * 16));

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes(outputUInt1, outputUInt2)));
                        break;
                    case 0x53:

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xFC, 0xFF, 0xFF, 0xFF, 0xFF, 0xFC, 0xF2, 0x79 }));
                        break;
                    case 0x54:

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xB9, 0x00, 0x03, 0x1D, 0x00, 0x44, 0x2D, 0x58 }));
                        break;
                    case 0x55:

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xB9, 0x00, 0x03, 0x1D, 0x00, 0x40, 0x4D, 0x4D }));
                        break;
                    case 0x56:

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xB7, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00 }));
                        break;
                    case 0x57:

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            new byte[] { 0xB6, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00 }));
                        break;
                    case 0x58:
                         //////NOOOOOTE: THIS IS POSSIBLE BROKEN REVIEW THIS PLEASE!!!!!//////
                        //DrawTriangle2
                        currentIndex++;
                        paramByte1 = bytes[currentIndex];
                        currentIndex++;
                        paramByte2 = bytes[currentIndex];

                        ushort combinedParamsss = (ushort)((paramByte1 << 8) | paramByte2);
                        ushort vv0 = (ushort)((combinedParamsss & 0x1F00) >> 8);
                        ushort vv1 = (ushort)(((combinedParamsss & 0x0003) << 3) | ((combinedParamsss & 0xE000) >> 13));
                        ushort vv2 = (ushort)((combinedParamsss & 0x007C) >> 2);
                        
                        currentIndex++;
                        paramByte1 = bytes[currentIndex];
                        currentIndex++;
                        paramByte2 = bytes[currentIndex];

                        combinedParamsss = (ushort)((paramByte1 << 8) | paramByte2);
                        ushort vv3 = (ushort)((combinedParamsss & 0x1F00) >> 8);
                        ushort vv4 = (ushort)(((combinedParamsss & 0x0003) << 3) | ((combinedParamsss & 0xE000) >> 13));
                        ushort vv5 = (ushort)((combinedParamsss & 0x007C) >> 2);

                        outputUInt1 = (uint)(0xB1000000 | (uint)((vv2 << 17) | (vv1 << 9) | (vv0 << 1)));
                        outputUInt2 = (uint)((vv5 << 17) | (vv4 << 9) | (vv3 << 1)); 

                        commands.Add(F3DEXCommandFactory.ReadCommand(commands.Count * 8,
                            ByteHelper.CombineIntoBytes(outputUInt1, outputUInt2)));
                        break;
                    case 0xFF:
                        //EOF
                        break;
                }

                currentIndex++;
            }

            return commands;
        }

        public static List<byte> CommandsToBytes(List<F3DEXCommand> commands)
        {
            List<Byte> bytes = new List<byte>();

            int currentCommandIndex = 0;

            while (currentCommandIndex < commands.Count)
            {
                switch (commands[currentCommandIndex].CommandID)
                {

                }

                currentCommandIndex++;
            }

            return bytes;
        }

    }
}
