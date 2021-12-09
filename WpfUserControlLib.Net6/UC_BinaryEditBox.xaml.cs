using LogUtils.Net;
using System.Windows.Controls;
using System.Windows.Input;
using VariousUtils.Net;
using WpfHelperClasses.Net6;

namespace WpfUserControlLib.Net6 {

    /// <summary>Interaction logic for UC_BinaryEditBox.xaml</summary>
    public partial class UC_BinaryEditBox : UC_UintEditBoxBase {

        private readonly ClassLog log = new ("UC_BinaryEditBox");

        public UC_BinaryEditBox() : base() {
            InitializeComponent();
        }

        public override string Text { get { return this.tbEdit.Text; } }


        protected override void DoSetValue(UInt64 value) {
            this.tbEdit.TextChanged -= this.TbEdit_TextChanged;
            int carretIndex = this.tbEdit.CaretIndex;
            //int existingSpaces = carretIndex / 4;
            this.tbEdit.Text = value.ToFormatedBinaryString().Trim();
            // A space added for every 4 binary digits. Adjust carret
            //int currentSpaces = this.tbEdit.Text.Length / 4;
            //carretIndex += (currentSpaces - existingSpaces);
            this.tbEdit.CaretIndex = carretIndex > this.tbEdit.Text.Length
                ? this.tbEdit.Text.Length
                : carretIndex;
            this.tbEdit.TextChanged += this.TbEdit_TextChanged;
        }


        protected override void DoSetEmpty() {
            this.tbEdit.TextChanged -= this.TbEdit_TextChanged;
            this.tbEdit.Text = "";
            this.tbEdit.TextChanged += this.TbEdit_TextChanged;
        }



        private void TbEdit_PreviewKeyDown(object sender, KeyEventArgs args) {
            this.ProcessPreviewKey(args,
                () => args.Key.IsBinaryNumericForbidden(),
                () => args.Key.IsNumeric(),
                () => {
                    string add = args.Key.GetNumericValue();
                    string newVal = tbEdit.PreviewKeyDownAssembleText(add);
                    newVal = newVal.Replace(" ", "");
                    this.log.Info("", () => string.Format("'{0}'  '{1}'  '{2}'", this.tbEdit, add, newVal));
                    if (newVal.Length > 0) {
                        this.ValidateRange(() => Convert.ToUInt64(newVal, 2).ToString(), args);
                    }
                });
        }


        private void TbEdit_TextChanged(object sender, TextChangedEventArgs e) {
            this.log.Info("tbEdit_TextChanged", this.tbEdit.Text);
            this.ProcessTextChanged(this.tbEdit.Text, () => Convert.ToUInt64(this.tbEdit.Text.Replace(" ", ""), 2));
        }
    }
}
