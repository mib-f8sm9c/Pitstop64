using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MK64Pitstop.Data.Karts;

namespace MK64Pitstop.Modules.Karts
{
    public partial class KartPreviewControl : ImagePreviewControl
    {
        //NEED TO: GET THE REFERENCE KART CODE WORKING!!

        public KartInfo Kart
        {
            get
            {
                return _kart;
            }
            set
            {
                StopPreview();
                _kart = value;
                if (_kart != null && _kart.KartAnimations.Count >= _animIndex)
                    _animIndex = 0;
                StartPreview();
            }
        }
        private KartInfo _kart = null;

        public KartInfo ReferenceKart
        {
            get
            {
                return _referenceKart;
            }
            set
            {
                if (_showReferenceKart)
                {
                    StopPreview();
                    _referenceKart = value;
                    StartPreview();
                }
                else
                    _referenceKart = value;

                cbOverlayKart.Enabled = (_referenceKart != null);
            }
        }
        private KartInfo _referenceKart = null;

        public int AnimIndex
        {
            get
            {
                return _animIndex;
            }
            set
            {
                StopPreview();
                _animIndex = value;
                StartPreview();
            }
        }
        private int _animIndex = 0;

        public int FrameIndex
        {
            get
            {
                return _frameIndex;
            }
            set
            {
                StopPreview();
                _frameIndex = value;
                StartPreview();
            }
        }
        private int _frameIndex = 0;
        private int _paletteIndex = 0;

        public int FramesPerSecond
        {
            get
            {
                return _fps;
            }
            set
            {
                StopPreview();
                _fps = value;
                _timer.Interval = (int)Math.Round(1000.0 / value);
                StartPreview();
            }
        }
        private int _fps;

        public enum PreviewMode
        {
            Static,
            Animated
        }
        public PreviewMode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                StopPreview();
                _mode = value;
                StartPreview();
            }
        }
        private PreviewMode _mode;

        public bool CycleAnimations { get; set; }

        public bool UseAnimPalettes
        {
            get
            {
                return _useAnimPalettes;
            }
            set
            {
                StopPreview();
                _useAnimPalettes = value;
                StartPreview();
            }
        }
        private bool _useAnimPalettes;

        public bool ShowReferenceKart
        {
            get
            {
                return _showReferenceKart;
            }
            set
            {
                StopPreview();
                _showReferenceKart = value;
                StartPreview();
            }
        }
        private bool _showReferenceKart;

        public bool DisplayRefKartOption
        {
            get
            {
                return cbOverlayKart.Visible;
            }
            set
            {
                cbOverlayKart.Visible = value;
            }
        }

        private Timer _timer;

        public KartPreviewControl()
        {
            InitializeComponent();

            _lockImageSize = true;

            //Debug for now
            _timer = new Timer();
            _timer.Tick += new EventHandler(_timer_Tick);
            FramesPerSecond = 30;
            Mode = PreviewMode.Static;
        }

        //Start the animation
        private void StartPreview()
        {
            if (this.DesignMode)
                return;

            if (Mode == PreviewMode.Animated && _kart != null)
            {
                if (!_timer.Enabled)
                    _timer.Start();
            }
            else
            {
                ResetPreview();
            }
        }

        //Stop the animation
        private void StopPreview()
        {
            if (this.DesignMode)
                return;

            if (_timer.Enabled)
                _timer.Stop();
        }

        //Reset the view to the new kart
        private void ResetPreview()
        {
            if (this.DesignMode)
                return;

            //Clear the image
            Image = null;
            OverlayImage = null;

            if (_kart == null || _kart.KartAnimations.Count == 0)
                return;

            if (_animIndex < 0 || _kart.KartAnimations.Count <= _animIndex ||
                _frameIndex < 0 || _kart.KartAnimations[_animIndex].OrderedImageNames.Count <= _frameIndex)
                    return;

            //Just display the single frame
            KartImage selectedKartImage = _kart.KartImages.Images[_kart.KartAnimations[_animIndex].OrderedImageNames[_frameIndex]];
            Image = selectedKartImage.Images[0].Image;

            if(ShowReferenceKart)
                SetReferenceImage();
        }

        private void SetReferenceImage()
        {
            if (_referenceKart == null)
                return;

            int imageIndex;
            KartAnimationSeries refAnim;
            int refFrameIndex;
            if (_kart.KartAnimations[_animIndex].IsTurnAnim)
                imageIndex = _kart.KartAnimations[_animIndex].GetImageIndexForTurnFrame(_frameIndex);
            else if (_kart.KartAnimations[_animIndex].IsSpinAnim)
                imageIndex = _kart.KartAnimations[_animIndex].GetImageIndexForSpinFrame(_frameIndex);
            else
                imageIndex = _kart.KartAnimations[_animIndex].GetImageIndexForCrashFrame(_frameIndex);

            refAnim = _referenceKart.KartAnimations.FirstOrDefault(a => (a.KartAnimationType & _kart.KartAnimations[_animIndex].KartAnimationType) != 0);
            if (refAnim == null)
                return;

            if (refAnim.IsTurnAnim)
                refFrameIndex = refAnim.GetTurnFrameForImageIndex(_frameIndex);
            else if (refAnim.IsSpinAnim)
                refFrameIndex = refAnim.GetSpinFrameForImageIndex(_frameIndex);
            else
                refFrameIndex = refAnim.GetCrashFrameForImageIndex(_frameIndex);

            KartImage selectedKartImage = _referenceKart.KartImages.Images[refAnim.OrderedImageNames[refFrameIndex]];
            OverlayImage = selectedKartImage.Images[0].Image;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_animIndex < 0 || _kart.KartAnimations.Count <= _animIndex)
                return;

            _frameIndex++;
            if (_kart.KartAnimations[_animIndex].OrderedImageNames.Count <= _frameIndex)
            {
                _frameIndex = 0;
                if (CycleAnimations)
                {
                    _animIndex++;
                    if (_animIndex >= _kart.KartAnimations.Count)
                        _animIndex = 0;
                }
            }
            ResetPreview();
        }

        private void cbOverlayKart_CheckedChanged(object sender, EventArgs e)
        {
            ShowReferenceKart = cbOverlayKart.Checked;
        }
    }
}
