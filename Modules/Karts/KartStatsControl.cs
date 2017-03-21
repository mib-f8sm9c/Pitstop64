using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pitstop64.Data.Karts;

namespace Pitstop64.Modules.Karts
{
    public partial class KartStatsControl : UserControl
    {
        public KartStats Stats
        {
            get
            {
                return _stats;
            }
            set
            {
                _stats = value;
                ResetForm();
            }
        }
        private KartStats _stats;

        public KartStatsControl()
            : this(null)
        {
        }

        public KartStatsControl(KartStats stats)
        {
            InitializeComponent();

            _stats = stats;

            ResetForm();
        }

        public void ResetForm()
        {
            if(_stats == null)
            {
                this.Enabled = false;
                //Clear all text boxes?
            }
            else
            {
                //Set all text boxes
                this.Enabled = true;

                //Settings 1
                txtWeight.Text = _stats.Weight.ToString();
                txtScale.Text = _stats.Scale.ToString();
                txtGravity.Text = _stats.Gravity.ToString();
                txtFriction.Text = _stats.Friction.ToString();
                txtTopSpeed.Text = _stats.TopSpeed.ToString();
                txtHandling.Text = _stats.Handling.ToString();
                txtHopHeight.Text = _stats.HopHeight.ToString();
                txtHopFall.Text = _stats.HopFallSpeed.ToString();
                txtBoundBox.Text = _stats.BoundingBox.ToString();
                txtSpeed50cc.Text = _stats.Speed50CC.ToString();
                txtSpeed100cc.Text = _stats.Speed100CC.ToString();
                txtSpeed150cc.Text = _stats.Speed150CC.ToString();
                txtSpeedExtra.Text = _stats.SpeedExtra.ToString();
                txtSpeedBattle.Text = _stats.SpeedBattle.ToString();
                txtTurnCoeff.Text = _stats.TurnSpeedReductionCoefficient.ToString();
                txtTurnCoeff2.Text = _stats.TurnSpeedReductionCoefficient2.ToString();

                //Unknowns
                txtUnk1.Text = _stats.Unknown1.ToString();
                txtUnk2.Text = _stats.Unknown2.ToString();
                txtUnk3.Text = _stats.Unknown3.ToString();
                txtUnk4.Text = _stats.Unknown4.ToString();
                txtUnk5.Text = _stats.Unknown5.ToString();
                txtUnk6.Text = _stats.Unknown6.ToString();
                txtUnk7.Text = _stats.Unknown7.ToString();
                txtUnk8.Text = _stats.Unknown8.ToString();
                txtUnk9.Text = _stats.Unknown9.ToString();
                txtUnk10.Text = _stats.Unknown10.ToString();
                txtUnk11.Text = _stats.Unknown11.ToString();
                txtUnk12.Text = _stats.Unknown12.ToString();
                txtUnk13.Text = _stats.Unknown13.ToString();
                txtUnk14.Text = _stats.Unknown14.ToString();
                txtUnk15.Text = _stats.Unknown15.ToString();
                txtUnk16.Text = _stats.Unknown16.ToString();
                txtUnk17.Text = _stats.Unknown17.ToString();
                txtUnk18.Text = _stats.Unknown18.ToString();
                txtUnk19.Text = _stats.Unknown19.ToString();
                txtUnk20.Text = _stats.Unknown20.ToString();
                txtUnk21.Text = _stats.Unknown21.ToString();
                txtUnk22.Text = _stats.Unknown22.ToString();
                txtUnk23.Text = _stats.Unknown23.ToString();
                txtUnk24.Text = _stats.Unknown24.ToString();
                txtUnk25.Text = _stats.Unknown25.ToString();
                txtUnk26.Text = _stats.Unknown26.ToString();

                //Accel Block
                txtAccel1.Text = _stats.AccelBlock[0].ToString();
                txtAccel2.Text = _stats.AccelBlock[1].ToString();
                txtAccel3.Text = _stats.AccelBlock[2].ToString();
                txtAccel4.Text = _stats.AccelBlock[3].ToString();
                txtAccel5.Text = _stats.AccelBlock[4].ToString();
                txtAccel6.Text = _stats.AccelBlock[5].ToString();
                txtAccel7.Text = _stats.AccelBlock[6].ToString();
                txtAccel8.Text = _stats.AccelBlock[7].ToString();
                txtAccel9.Text = _stats.AccelBlock[8].ToString();
                txtAccel10.Text = _stats.AccelBlock[9].ToString();

                //Block 1
                txt1Block1.Text = _stats.Block1Unknowns[0].ToString();
                txt1Block2.Text = _stats.Block1Unknowns[1].ToString();
                txt1Block3.Text = _stats.Block1Unknowns[2].ToString();
                txt1Block4.Text = _stats.Block1Unknowns[3].ToString();
                txt1Block5.Text = _stats.Block1Unknowns[4].ToString();
                txt1Block6.Text = _stats.Block1Unknowns[5].ToString();
                txt1Block7.Text = _stats.Block1Unknowns[6].ToString();
                txt1Block8.Text = _stats.Block1Unknowns[7].ToString();
                txt1Block9.Text = _stats.Block1Unknowns[8].ToString();
                txt1Block10.Text = _stats.Block1Unknowns[9].ToString();
                txt1Block11.Text = _stats.Block1Unknowns[10].ToString();
                txt1Block12.Text = _stats.Block1Unknowns[11].ToString();
                txt1Block13.Text = _stats.Block1Unknowns[12].ToString();
                txt1Block14.Text = _stats.Block1Unknowns[13].ToString();
                txt1Block15.Text = _stats.Block1Unknowns[14].ToString();

                //Block 2
                txt2Block1.Text = _stats.Block2Unknowns[0].ToString();
                txt2Block2.Text = _stats.Block2Unknowns[1].ToString();
                txt2Block3.Text = _stats.Block2Unknowns[2].ToString();
                txt2Block4.Text = _stats.Block2Unknowns[3].ToString();
                txt2Block5.Text = _stats.Block2Unknowns[4].ToString();
                txt2Block6.Text = _stats.Block2Unknowns[5].ToString();
                txt2Block7.Text = _stats.Block2Unknowns[6].ToString();
                txt2Block8.Text = _stats.Block2Unknowns[7].ToString();
                txt2Block9.Text = _stats.Block2Unknowns[8].ToString();
                txt2Block10.Text = _stats.Block2Unknowns[9].ToString();
                txt2Block11.Text = _stats.Block2Unknowns[10].ToString();
                txt2Block12.Text = _stats.Block2Unknowns[11].ToString();
                txt2Block13.Text = _stats.Block2Unknowns[12].ToString();
                txt2Block14.Text = _stats.Block2Unknowns[13].ToString();
                txt2Block15.Text = _stats.Block2Unknowns[14].ToString();

                //Block 3
                txt3Block1.Text = _stats.Block3Unknowns[0].ToString();
                txt3Block2.Text = _stats.Block3Unknowns[1].ToString();
                txt3Block3.Text = _stats.Block3Unknowns[2].ToString();
                txt3Block4.Text = _stats.Block3Unknowns[3].ToString();
                txt3Block5.Text = _stats.Block3Unknowns[4].ToString();
                txt3Block6.Text = _stats.Block3Unknowns[5].ToString();
                txt3Block7.Text = _stats.Block3Unknowns[6].ToString();
                txt3Block8.Text = _stats.Block3Unknowns[7].ToString();
                txt3Block9.Text = _stats.Block3Unknowns[8].ToString();
                txt3Block10.Text = _stats.Block3Unknowns[9].ToString();
                txt3Block11.Text = _stats.Block3Unknowns[10].ToString();
                txt3Block12.Text = _stats.Block3Unknowns[11].ToString();
                txt3Block13.Text = _stats.Block3Unknowns[12].ToString();
                txt3Block14.Text = _stats.Block3Unknowns[13].ToString();
                txt3Block15.Text = _stats.Block3Unknowns[14].ToString();

                //Block 4
                txt4Block1.Text = _stats.Block4Unknowns[0].ToString();
                txt4Block2.Text = _stats.Block4Unknowns[1].ToString();
                txt4Block3.Text = _stats.Block4Unknowns[2].ToString();
                txt4Block4.Text = _stats.Block4Unknowns[3].ToString();
                txt4Block5.Text = _stats.Block4Unknowns[4].ToString();
                txt4Block6.Text = _stats.Block4Unknowns[5].ToString();
                txt4Block7.Text = _stats.Block4Unknowns[6].ToString();
                txt4Block8.Text = _stats.Block4Unknowns[7].ToString();
                txt4Block9.Text = _stats.Block4Unknowns[8].ToString();
                txt4Block10.Text = _stats.Block4Unknowns[9].ToString();
                txt4Block11.Text = _stats.Block4Unknowns[10].ToString();
                txt4Block12.Text = _stats.Block4Unknowns[11].ToString();
                txt4Block13.Text = _stats.Block4Unknowns[12].ToString();
                txt4Block14.Text = _stats.Block4Unknowns[13].ToString();
                txt4Block15.Text = _stats.Block4Unknowns[14].ToString();

                //Block 5
                txt5Block1.Text = _stats.Block5Unknowns[0].ToString();
                txt5Block2.Text = _stats.Block5Unknowns[1].ToString();
                txt5Block3.Text = _stats.Block5Unknowns[2].ToString();
                txt5Block4.Text = _stats.Block5Unknowns[3].ToString();
                txt5Block5.Text = _stats.Block5Unknowns[4].ToString();
                txt5Block6.Text = _stats.Block5Unknowns[5].ToString();
                txt5Block7.Text = _stats.Block5Unknowns[6].ToString();
                txt5Block8.Text = _stats.Block5Unknowns[7].ToString();
                txt5Block9.Text = _stats.Block5Unknowns[8].ToString();
                txt5Block10.Text = _stats.Block5Unknowns[9].ToString();
                txt5Block11.Text = _stats.Block5Unknowns[10].ToString();
                txt5Block12.Text = _stats.Block5Unknowns[11].ToString();
                txt5Block13.Text = _stats.Block5Unknowns[12].ToString();
                txt5Block14.Text = _stats.Block5Unknowns[13].ToString();
                txt5Block15.Text = _stats.Block5Unknowns[14].ToString();

                //Block 6
                txt6Block1.Text = _stats.Block6Unknowns[0].ToString();
                txt6Block2.Text = _stats.Block6Unknowns[1].ToString();
                txt6Block3.Text = _stats.Block6Unknowns[2].ToString();
                txt6Block4.Text = _stats.Block6Unknowns[3].ToString();
                txt6Block5.Text = _stats.Block6Unknowns[4].ToString();
                txt6Block6.Text = _stats.Block6Unknowns[5].ToString();
                txt6Block7.Text = _stats.Block6Unknowns[6].ToString();
                txt6Block8.Text = _stats.Block6Unknowns[7].ToString();
                txt6Block9.Text = _stats.Block6Unknowns[8].ToString();
                txt6Block10.Text = _stats.Block6Unknowns[9].ToString();
                txt6Block11.Text = _stats.Block6Unknowns[10].ToString();
                txt6Block12.Text = _stats.Block6Unknowns[11].ToString();
                txt6Block13.Text = _stats.Block6Unknowns[12].ToString();
                txt6Block14.Text = _stats.Block6Unknowns[13].ToString();
                txt6Block15.Text = _stats.Block6Unknowns[14].ToString();

                //Block 7
                txt7Block1.Text = _stats.Block7Unknowns[0].ToString();
                txt7Block2.Text = _stats.Block7Unknowns[1].ToString();
                txt7Block3.Text = _stats.Block7Unknowns[2].ToString();
                txt7Block4.Text = _stats.Block7Unknowns[3].ToString();
                txt7Block5.Text = _stats.Block7Unknowns[4].ToString();
                txt7Block6.Text = _stats.Block7Unknowns[5].ToString();
                txt7Block7.Text = _stats.Block7Unknowns[6].ToString();
                txt7Block8.Text = _stats.Block7Unknowns[7].ToString();
                txt7Block9.Text = _stats.Block7Unknowns[8].ToString();
                txt7Block10.Text = _stats.Block7Unknowns[9].ToString();
                txt7Block11.Text = _stats.Block7Unknowns[10].ToString();
                txt7Block12.Text = _stats.Block7Unknowns[11].ToString();
                txt7Block13.Text = _stats.Block7Unknowns[12].ToString();
                txt7Block14.Text = _stats.Block7Unknowns[13].ToString();
                txt7Block15.Text = _stats.Block7Unknowns[14].ToString();

            }
        }

        public void Save()
        {
            if (_stats == null)
                return;

            //Save all info to the stats

            try
            {
                //Settings 1
                _stats.Weight = float.Parse(txtWeight.Text);
                _stats.Scale = float.Parse(txtScale.Text);
                _stats.Gravity = float.Parse(txtGravity.Text);
                _stats.Friction = float.Parse(txtFriction.Text);
                _stats.TopSpeed = float.Parse(txtTopSpeed.Text);
                _stats.Handling = float.Parse(txtHandling.Text);
                _stats.HopHeight = float.Parse(txtHopHeight.Text);
                _stats.HopFallSpeed = float.Parse(txtHopFall.Text);
                _stats.BoundingBox = float.Parse(txtBoundBox.Text);
                _stats.Speed50CC = float.Parse(txtSpeed50cc.Text);
                _stats.Speed100CC = float.Parse(txtSpeed100cc.Text);
                _stats.Speed150CC = float.Parse(txtSpeed150cc.Text);
                _stats.SpeedExtra = float.Parse(txtSpeedExtra.Text);
                _stats.SpeedBattle = float.Parse(txtSpeedBattle.Text);
                _stats.TurnSpeedReductionCoefficient = float.Parse(txtTurnCoeff.Text);
                _stats.TurnSpeedReductionCoefficient2 = float.Parse(txtTurnCoeff2.Text);

                //Unknowns
                _stats.Unknown1 = float.Parse(txtUnk1.Text);
                _stats.Unknown2 = float.Parse(txtUnk2.Text);
                _stats.Unknown3 = float.Parse(txtUnk3.Text);
                _stats.Unknown4 = float.Parse(txtUnk4.Text);
                _stats.Unknown5 = float.Parse(txtUnk5.Text);
                _stats.Unknown6 = float.Parse(txtUnk6.Text);
                _stats.Unknown7 = float.Parse(txtUnk7.Text);
                _stats.Unknown8 = float.Parse(txtUnk8.Text);
                _stats.Unknown9 = float.Parse(txtUnk9.Text);
                _stats.Unknown10 = float.Parse(txtUnk10.Text);
                _stats.Unknown11 = float.Parse(txtUnk11.Text);
                _stats.Unknown12 = float.Parse(txtUnk12.Text);
                _stats.Unknown13 = float.Parse(txtUnk13.Text);
                _stats.Unknown14 = float.Parse(txtUnk14.Text);
                _stats.Unknown15 = float.Parse(txtUnk15.Text);
                _stats.Unknown16 = float.Parse(txtUnk16.Text);
                _stats.Unknown17 = float.Parse(txtUnk17.Text);
                _stats.Unknown18 = float.Parse(txtUnk18.Text);
                _stats.Unknown19 = float.Parse(txtUnk19.Text);
                _stats.Unknown20 = float.Parse(txtUnk20.Text);
                _stats.Unknown21 = float.Parse(txtUnk21.Text);
                _stats.Unknown22 = float.Parse(txtUnk22.Text);
                _stats.Unknown23 = float.Parse(txtUnk23.Text);
                _stats.Unknown24 = float.Parse(txtUnk24.Text);
                _stats.Unknown25 = float.Parse(txtUnk25.Text);
                _stats.Unknown26 = float.Parse(txtUnk26.Text);

                //Accel Block
                _stats.AccelBlock[0] = float.Parse(txtAccel1.Text);
                _stats.AccelBlock[1] = float.Parse(txtAccel2.Text);
                _stats.AccelBlock[2] = float.Parse(txtAccel3.Text);
                _stats.AccelBlock[3] = float.Parse(txtAccel4.Text);
                _stats.AccelBlock[4] = float.Parse(txtAccel5.Text);
                _stats.AccelBlock[5] = float.Parse(txtAccel6.Text);
                _stats.AccelBlock[6] = float.Parse(txtAccel7.Text);
                _stats.AccelBlock[7] = float.Parse(txtAccel8.Text);
                _stats.AccelBlock[8] = float.Parse(txtAccel9.Text);
                _stats.AccelBlock[9] = float.Parse(txtAccel10.Text);

                //Block 1
                _stats.Block1Unknowns[0] = float.Parse(txt1Block1.Text);
                _stats.Block1Unknowns[1] = float.Parse(txt1Block2.Text);
                _stats.Block1Unknowns[2] = float.Parse(txt1Block3.Text);
                _stats.Block1Unknowns[3] = float.Parse(txt1Block4.Text);
                _stats.Block1Unknowns[4] = float.Parse(txt1Block5.Text);
                _stats.Block1Unknowns[5] = float.Parse(txt1Block6.Text);
                _stats.Block1Unknowns[6] = float.Parse(txt1Block7.Text);
                _stats.Block1Unknowns[7] = float.Parse(txt1Block8.Text);
                _stats.Block1Unknowns[8] = float.Parse(txt1Block9.Text);
                _stats.Block1Unknowns[9] = float.Parse(txt1Block10.Text);
                _stats.Block1Unknowns[10] = float.Parse(txt1Block11.Text);
                _stats.Block1Unknowns[11] = float.Parse(txt1Block12.Text);
                _stats.Block1Unknowns[12] = float.Parse(txt1Block13.Text);
                _stats.Block1Unknowns[13] = float.Parse(txt1Block14.Text);
                _stats.Block1Unknowns[14] = float.Parse(txt1Block15.Text);

                //Block 2
                _stats.Block2Unknowns[0] = float.Parse(txt2Block1.Text);
                _stats.Block2Unknowns[1] = float.Parse(txt2Block2.Text);
                _stats.Block2Unknowns[2] = float.Parse(txt2Block3.Text);
                _stats.Block2Unknowns[3] = float.Parse(txt2Block4.Text);
                _stats.Block2Unknowns[4] = float.Parse(txt2Block5.Text);
                _stats.Block2Unknowns[5] = float.Parse(txt2Block6.Text);
                _stats.Block2Unknowns[6] = float.Parse(txt2Block7.Text);
                _stats.Block2Unknowns[7] = float.Parse(txt2Block8.Text);
                _stats.Block2Unknowns[8] = float.Parse(txt2Block9.Text);
                _stats.Block2Unknowns[9] = float.Parse(txt2Block10.Text);
                _stats.Block2Unknowns[10] = float.Parse(txt2Block11.Text);
                _stats.Block2Unknowns[11] = float.Parse(txt2Block12.Text);
                _stats.Block2Unknowns[12] = float.Parse(txt2Block13.Text);
                _stats.Block2Unknowns[13] = float.Parse(txt2Block14.Text);
                _stats.Block2Unknowns[14] = float.Parse(txt2Block15.Text);

                //Block 3
                _stats.Block3Unknowns[0] = float.Parse(txt3Block1.Text);
                _stats.Block3Unknowns[1] = float.Parse(txt3Block2.Text);
                _stats.Block3Unknowns[2] = float.Parse(txt3Block3.Text);
                _stats.Block3Unknowns[3] = float.Parse(txt3Block4.Text);
                _stats.Block3Unknowns[4] = float.Parse(txt3Block5.Text);
                _stats.Block3Unknowns[5] = float.Parse(txt3Block6.Text);
                _stats.Block3Unknowns[6] = float.Parse(txt3Block7.Text);
                _stats.Block3Unknowns[7] = float.Parse(txt3Block8.Text);
                _stats.Block3Unknowns[8] = float.Parse(txt3Block9.Text);
                _stats.Block3Unknowns[9] = float.Parse(txt3Block10.Text);
                _stats.Block3Unknowns[10] = float.Parse(txt3Block11.Text);
                _stats.Block3Unknowns[11] = float.Parse(txt3Block12.Text);
                _stats.Block3Unknowns[12] = float.Parse(txt3Block13.Text);
                _stats.Block3Unknowns[13] = float.Parse(txt3Block14.Text);
                _stats.Block3Unknowns[14] = float.Parse(txt3Block15.Text);

                //Block 4
                _stats.Block4Unknowns[0] = float.Parse(txt4Block1.Text);
                _stats.Block4Unknowns[1] = float.Parse(txt4Block2.Text);
                _stats.Block4Unknowns[2] = float.Parse(txt4Block3.Text);
                _stats.Block4Unknowns[3] = float.Parse(txt4Block4.Text);
                _stats.Block4Unknowns[4] = float.Parse(txt4Block5.Text);
                _stats.Block4Unknowns[5] = float.Parse(txt4Block6.Text);
                _stats.Block4Unknowns[6] = float.Parse(txt4Block7.Text);
                _stats.Block4Unknowns[7] = float.Parse(txt4Block8.Text);
                _stats.Block4Unknowns[8] = float.Parse(txt4Block9.Text);
                _stats.Block4Unknowns[9] = float.Parse(txt4Block10.Text);
                _stats.Block4Unknowns[10] = float.Parse(txt4Block11.Text);
                _stats.Block4Unknowns[11] = float.Parse(txt4Block12.Text);
                _stats.Block4Unknowns[12] = float.Parse(txt4Block13.Text);
                _stats.Block4Unknowns[13] = float.Parse(txt4Block14.Text);
                _stats.Block4Unknowns[14] = float.Parse(txt4Block15.Text);

                //Block 5
                _stats.Block5Unknowns[0] = float.Parse(txt5Block1.Text);
                _stats.Block5Unknowns[1] = float.Parse(txt5Block2.Text);
                _stats.Block5Unknowns[2] = float.Parse(txt5Block3.Text);
                _stats.Block5Unknowns[3] = float.Parse(txt5Block4.Text);
                _stats.Block5Unknowns[4] = float.Parse(txt5Block5.Text);
                _stats.Block5Unknowns[5] = float.Parse(txt5Block6.Text);
                _stats.Block5Unknowns[6] = float.Parse(txt5Block7.Text);
                _stats.Block5Unknowns[7] = float.Parse(txt5Block8.Text);
                _stats.Block5Unknowns[8] = float.Parse(txt5Block9.Text);
                _stats.Block5Unknowns[9] = float.Parse(txt5Block10.Text);
                _stats.Block5Unknowns[10] = float.Parse(txt5Block11.Text);
                _stats.Block5Unknowns[11] = float.Parse(txt5Block12.Text);
                _stats.Block5Unknowns[12] = float.Parse(txt5Block13.Text);
                _stats.Block5Unknowns[13] = float.Parse(txt5Block14.Text);
                _stats.Block5Unknowns[14] = float.Parse(txt5Block15.Text);

                //Block 6
                _stats.Block6Unknowns[0] = float.Parse(txt6Block1.Text);
                _stats.Block6Unknowns[1] = float.Parse(txt6Block2.Text);
                _stats.Block6Unknowns[2] = float.Parse(txt6Block3.Text);
                _stats.Block6Unknowns[3] = float.Parse(txt6Block4.Text);
                _stats.Block6Unknowns[4] = float.Parse(txt6Block5.Text);
                _stats.Block6Unknowns[5] = float.Parse(txt6Block6.Text);
                _stats.Block6Unknowns[6] = float.Parse(txt6Block7.Text);
                _stats.Block6Unknowns[7] = float.Parse(txt6Block8.Text);
                _stats.Block6Unknowns[8] = float.Parse(txt6Block9.Text);
                _stats.Block6Unknowns[9] = float.Parse(txt6Block10.Text);
                _stats.Block6Unknowns[10] = float.Parse(txt6Block11.Text);
                _stats.Block6Unknowns[11] = float.Parse(txt6Block12.Text);
                _stats.Block6Unknowns[12] = float.Parse(txt6Block13.Text);
                _stats.Block6Unknowns[13] = float.Parse(txt6Block14.Text);
                _stats.Block6Unknowns[14] = float.Parse(txt6Block15.Text);

                //Block 7
                _stats.Block7Unknowns[0] = float.Parse(txt7Block1.Text);
                _stats.Block7Unknowns[1] = float.Parse(txt7Block2.Text);
                _stats.Block7Unknowns[2] = float.Parse(txt7Block3.Text);
                _stats.Block7Unknowns[3] = float.Parse(txt7Block4.Text);
                _stats.Block7Unknowns[4] = float.Parse(txt7Block5.Text);
                _stats.Block7Unknowns[5] = float.Parse(txt7Block6.Text);
                _stats.Block7Unknowns[6] = float.Parse(txt7Block7.Text);
                _stats.Block7Unknowns[7] = float.Parse(txt7Block8.Text);
                _stats.Block7Unknowns[8] = float.Parse(txt7Block9.Text);
                _stats.Block7Unknowns[9] = float.Parse(txt7Block10.Text);
                _stats.Block7Unknowns[10] = float.Parse(txt7Block11.Text);
                _stats.Block7Unknowns[11] = float.Parse(txt7Block12.Text);
                _stats.Block7Unknowns[12] = float.Parse(txt7Block13.Text);
                _stats.Block7Unknowns[13] = float.Parse(txt7Block14.Text);
                _stats.Block7Unknowns[14] = float.Parse(txt7Block15.Text);
            }
            catch(Exception e)
            {
                //failure
                MessageBox.Show("Failed to save settings! Check for invalid entries.");
            }
        }

    }
}
