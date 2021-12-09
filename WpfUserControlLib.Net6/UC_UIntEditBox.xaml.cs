using LogUtils.Net;
using System.Windows.Controls;
using System.Windows.Input;
using WpfHelperClasses.Net6;

namespace WpfUserControlLib.Net6 {

    /// <summary>Interaction logic for UC_UIntEditBox.xaml</summary>
    public partial class UC_UIntEditBox : UC_UintEditBoxBase {

        private readonly ClassLog log = new ("UC_UIntEditBox");

        public UC_UIntEditBox() : base() {
            InitializeComponent();
        }

        public override string Text { get { return this.tbEdit.Text; } }

        protected override void DoSetValue(UInt64 value) {
            this.tbEdit.TextChanged -= this.TbEdit_TextChanged;
            int carretIndex = this.tbEdit.CaretIndex;
            this.tbEdit.Text = value.ToString();
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
                () => args.Key.IsUnsignedNumericForbidden(),
                () => args.Key.IsNumeric(),
                () => {
                    string add = args.Key.GetNumericValue();
                    string newVal = this.tbEdit.PreviewKeyDownAssembleText(add);
                    this.log.Info("", () => string.Format("'{0}'  '{1}'  '{2}'", this.tbEdit, add, newVal));
                    if (newVal.Length > 0) {
                        this.ValidateRange(() => { return newVal; }, args);
                    }
                });
        }


        private void TbEdit_TextChanged(object sender, TextChangedEventArgs e) {
            this.log.Info("tbEdit_TextChanged", this.tbEdit.Text);
            this.ProcessTextChanged(this.tbEdit.Text, () => Convert.ToUInt64(this.tbEdit.Text));
        }
    }
}
