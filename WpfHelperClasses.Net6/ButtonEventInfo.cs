using System.Windows.Controls;

namespace WpfHelperClasses.Net6 {

    /// <summary>Manages information on one Button</summary>
    public class ButtonEventInfo {

        #region Properties

        /// <summary>The Button held by this object</summary>
        public Button ButtonObj { get; private set; }

        /// <summary>
        /// Indicates if the contained Button has received the SizeChanged event
        /// </summary>
        public bool IsSized { get; private set; }

        #endregion

        #region Constructors

        /// <summary>Constructor</summary>
        /// <param name="button">The button that is managed</param>
        public ButtonEventInfo(Button button) {
            this.ButtonObj = button;
            this.IsSized = false;
        }

        #endregion

        #region Public

        /// <summary>Will set itself sized if it holds the same button</summary>
        /// <param name="button">The Button that is saved</param>
        public void SetSizedIfSame() {
            // Will cause resize multiple times, once for each button in set but no way to compare otherwise
            this.IsSized = true;
        }


        /// <summary>Reset the object to not yet sized</summary>
        public void ResetSized() {
            this.IsSized = false;
        }

        #endregion

    }
}
