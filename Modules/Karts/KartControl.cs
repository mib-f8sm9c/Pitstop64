using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Data;
using Cereal64.Common.Utils.Encoding;
using Cereal64.Microcodes.F3DEX.DataElements;

namespace MK64Pitstop.Modules.Karts
{
    public partial class KartControl : UserControl
    {
        private KartGraphicsReferenceBlock _kartInfo;

        public KartControl()
        {
            InitializeComponent();

            string[] animNames = Enum.GetNames(typeof(KartAnimationSeries.KartAnimationTypeFlag));

            foreach (string anim in animNames)
            {
                cbAnimation.Items.Add(anim);
            }
        }

        public void UpdateReferences(KartGraphicsReferenceBlock info)
        {
            _kartInfo = info;

            cbCharacter.Items.Clear();

            for (int i = 0; i < info.Karts.Count; i++)
                cbCharacter.Items.Add(info.Karts[i].KartName);

            cbCharacter.SelectedIndex = 0;
        }

        private void DisplaySelectedImage()
        {
            int characterIndex = cbCharacter.SelectedIndex;
            int imageIndex = cbImageNum.SelectedIndex;

            KartAnimationSeries.KartAnimationTypeFlag animFlag = (KartAnimationSeries.KartAnimationTypeFlag)Math.Pow(2, cbAnimation.SelectedIndex);
            
            if (characterIndex < 0 || characterIndex >= _kartInfo.Karts.Count)
            {
                pictureBox.Image = null;
                return;
            }

            KartAnimationSeries selectedAnim = _kartInfo.Karts[characterIndex].KartAnimations.FirstOrDefault(f => f.KartAnimationType == (int)animFlag);

            if (selectedAnim == null)
            {
                pictureBox.Image = null;
                return;
            }

            if (imageIndex < 0 || imageIndex >= selectedAnim.OrderedImageNames.Count)
            {
                pictureBox.Image = null;
                return;
            }

            pictureBox.Image = _kartInfo.Karts[characterIndex].KartImagePool[selectedAnim.OrderedImageNames[imageIndex]].Image;
        }

        private void cbImageNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySelectedImage();
        }

        private void cbAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbImageNum.Items.Clear();

            int characterIndex = cbCharacter.SelectedIndex;
            KartAnimationSeries.KartAnimationTypeFlag animFlag = (KartAnimationSeries.KartAnimationTypeFlag)Math.Pow(2, cbAnimation.SelectedIndex);
            int animIndex = cbAnimation.SelectedIndex;

            if (characterIndex < 0 || characterIndex >= _kartInfo.Karts.Count)
            {
                pictureBox.Image = null;
                return;
            }

            KartAnimationSeries selectedAnim = _kartInfo.Karts[characterIndex].KartAnimations.FirstOrDefault(f => f.KartAnimationType == (int)animFlag);
            
            if(selectedAnim == null)
            {
                pictureBox.Image = null;
                return;
            }

            foreach (string str in selectedAnim.OrderedImageNames)
            {
                cbImageNum.Items.Add(str);
            }

            if(cbImageNum.Items.Count > 0)
                cbImageNum.SelectedIndex = 0;
        }

        private void cbCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Since animations are constant, no need to change it
            if (cbAnimation.SelectedIndex == 0)
                cbAnimation_SelectedIndexChanged(sender, e);
            else
                cbAnimation.SelectedIndex = 9;
        }

        /*
        private void cbCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbImageNum.Items.Clear();

            MarioKartRomInfo.OriginalCharacters character = (MarioKartRomInfo.OriginalCharacters)cbCharacter.SelectedIndex;
            List<MIO0Block> blocks = new List<MIO0Block>();
            //switch (character)
            //{
            //    case MarioKartRomInfo.OriginalCharacters.Mario:
            //        for (int j = 0; j < _kartInfo.Mario1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Mario1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Mario2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Mario2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Luigi:
            //        for (int j = 0; j < _kartInfo.Luigi1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Luigi1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Luigi2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Luigi2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Bowser:
            //        for (int j = 0; j < _kartInfo.Bowser1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Bowser1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Bowser2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Bowser2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Toad:
            //        for (int j = 0; j < _kartInfo.Toad1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Toad1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Toad2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Toad2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Yoshi:
            //        for (int j = 0; j < _kartInfo.Yoshi1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Yoshi1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Yoshi2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Yoshi2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.DK:
            //        for (int j = 0; j < _kartInfo.DK1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.DK1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.DK2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.DK2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Peach:
            //        for (int j = 0; j < _kartInfo.Peach1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Peach1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Peach2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Peach2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //    case MarioKartRomInfo.OriginalCharacters.Wario:
            //        for (int j = 0; j < _kartInfo.Wario1References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Wario1References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        for (int j = 0; j < _kartInfo.Wario2References.Length; j++)
            //            cbImageNum.Items.Add(((Texture)((MIO0Block)_kartInfo.Wario2References[j].ReferenceElement).DecodedN64DataElement).Image);
            //        break;
            //}

            DisplaySelectedImage();



            switch (character)
            {
                case MarioKartRomInfo.OriginalCharacters.Mario:
                    for (int j = 0; j < _kartInfo.Mario1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Mario1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Mario1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Mario2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Mario2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Mario2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Luigi:
                    for (int j = 0; j < _kartInfo.Luigi1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Luigi1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Luigi1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Luigi2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Luigi2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Luigi2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Bowser:
                    for (int j = 0; j < _kartInfo.Bowser1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Bowser1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Bowser1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Bowser2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Bowser2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Bowser2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Toad:
                    for (int j = 0; j < _kartInfo.Toad1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Toad1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Toad1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Toad2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Toad2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Toad2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Yoshi:
                    for (int j = 0; j < _kartInfo.Yoshi1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Yoshi1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Yoshi1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Yoshi2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Yoshi2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Yoshi2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.DK:
                    for (int j = 0; j < _kartInfo.DK1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.DK1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.DK1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.DK2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.DK2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.DK2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Peach:
                    for (int j = 0; j < _kartInfo.Peach1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Peach1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Peach1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Peach2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Peach2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Peach2References[j].ReferenceElement);
                    }
                    break;
                case MarioKartRomInfo.OriginalCharacters.Wario:
                    for (int j = 0; j < _kartInfo.Wario1References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Wario1References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Wario1References[j].ReferenceElement);
                    }
                    for (int j = 0; j < _kartInfo.Wario2References.Length; j++)
                    {
                        if (!blocks.Contains((MIO0Block)_kartInfo.Wario2References[j].ReferenceElement))
                            blocks.Add((MIO0Block)_kartInfo.Wario2References[j].ReferenceElement);
                    }
                    break;
            }

            List<MIO0Block> unsortedBlocks = new List<MIO0Block>(blocks);

            blocks.Sort((b1, b2) => b1.FileOffset.CompareTo(b2.FileOffset));

            List<int> orderOfBlocks = new List<int>();

            foreach (MIO0Block block in blocks)
            {
                orderOfBlocks.Add(unsortedBlocks.IndexOf(block));
            }

            foreach (MIO0Block block in blocks)
                cbImageNum.Items.Add(((Texture)block.DecodedN64DataElement).Image);

            cbImageNum.SelectedIndex = 0;
        }
        */

    }
}
