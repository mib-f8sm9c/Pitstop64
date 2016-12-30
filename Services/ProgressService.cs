using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pitstop64.Services
{
    public class ProgressService
    {
        private static ProgressDialog _dialog;

        //event for if the dialog is cancelled?
        public delegate void CancelProgressEvent();

        public static event CancelProgressEvent ProgressCancelled = delegate { };

        public static bool StartDialog(string message = "Loading")
        {
            if (_dialog != null)
                return false;

            _dialog = new ProgressDialog(message);
            _dialog.FormClosed += new System.Windows.Forms.FormClosedEventHandler(_dialog_FormClosed);

            _dialog.TopMost = true;
            _dialog.Show();

            return true;
        }

        public static void SetMessage(string message)
        {
            if (_dialog == null)
                return;

            _dialog.Message = message;
        }

        public static void StopDialog(DialogResult result = DialogResult.OK)
        {
            if (_dialog == null)
                return;

            _dialog.CloseProgressDialog(result);
        }

        private static void _dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_dialog.DialogResult == DialogResult.OK)
            {
                //Good result, closed through StopDialog
            }
            else
            {
                //Bad result, closed through Cancel
                ProgressCancelled();
            }

            //Either way, finish
            _dialog = null;
        }
    }
}
