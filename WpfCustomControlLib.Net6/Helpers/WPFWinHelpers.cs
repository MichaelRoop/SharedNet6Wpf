using ChkUtils.Net;
using LogUtils.Net;
using System.Windows;
using System.Windows.Controls;

namespace WpfCustomControlLib.Net6.Helpers {

    public static class WPFWinHelpers {

        static readonly ClassLog LOG = new ("WindowHelpers");

        /// <summary> 
        /// Call in MyWindowStyle styled Window OnApplyTemplate to bind drag and move
        /// </summary>
        /// <remarks>Call in the overriden OnApplyTemplate function</remarks>
        /// <param name="win">The window calling this function</param>
        public static void BindMouseDownToCustomTitleBar(this Window win) {
            try {
                if (win != null) {
                    if (win.Template.FindName("brdTitle", win) is Border b) {
                        b.MouseDown += (sender, args) => {
                            WrapErr.ToErrReport(9999, "Drag when mouse not down", () => {
                                if (win.WindowState == WindowState.Maximized) {
                                    // Dislodge it from maximized state to move
                                    win.WindowState = WindowState.Normal;

                                    // Center the window on the click point
                                    Point p = args.GetPosition(win);
                                    win.Top = p.Y - 15; // Middle of top bar
                                    win.Left = p.X - (win.Width / 2.0);
                                    win.DragMove();
                                }
                                else {
                                    win.DragMove();
                                }
                            });
                        };
                    }
                    else {
                        LOG.Error(9999, "Could not find PART_topBar - are you sure you have style set to MyWindowStyle?");
                    }
                }
            }
            catch (Exception e) {
                LOG.Exception(9999, "BindMouseDownToCustomTitleBar", "", e);
            }
        }



        /// <summary>Hide the title bar icon on the MyWindow styled window</summary>
        /// <param name="win">The window with the title bar</param>
        public static void HideTitleBarIcon(this Window win) {
            try {
                if (win != null) {
                    if (win.Template.FindName("PART_IconBorder", win) is Border b) {
                        b.Visibility = Visibility.Collapsed;
                    }
                    else {
                        LOG.Error(9999, "Could not find PART_IconBorder - are you sure you have style set to MyWindowStyle?");
                    }
                }
            }
            catch (Exception e) {
                LOG.Exception(9999, "HideTitleBarIcon", "", e);
            }
        }


    }

}
