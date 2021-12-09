using System.Windows.Controls;

namespace WpfHelperClasses.Net6 {

    public static class WpfTextBoxExtensions {

        /// <summary>Assemble string for validation based on insertion pont of character</summary>
        /// <param name="tb">The edit box with original value</param>
        /// <param name="add">The additional character</param>
        /// <returns>Assebled string with new character inserted in indexed location</returns>
        public static string PreviewKeyDownAssembleText(this TextBox tb, string add) {
            int pos = tb.CaretIndex;
            if (pos == tb.Text.Length) {
                return string.Format("{0}{1}", tb.Text, add);
            }
            else {
                string newValue = tb.Text;
                return newValue.Insert(pos, add);
            }
        }

    }
}
