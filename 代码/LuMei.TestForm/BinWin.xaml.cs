using LuMei.FileLibrary.BinFile;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LuMei.TestForm
{
    /// <summary>
    /// BinWin.xaml 的交互逻辑
    /// </summary>
    public partial class BinWin : Window
    {
        public BinFile binFile;
        public BinWin.BinTable myBinTable;
        public TreeViewItem selectedItem;
        public BinFileValue addedValue;
        public BinWin()
        {
            InitializeComponent();
        }
        private void BinWin_Loaded(object sender, RoutedEventArgs e)
        {
            checked
            {
                this.binFile = new BinFile(@"F:\Project\Code\LuMei\代码\LuMei.TestForm\Katarina.bin");
                if (this.binFile != null)
                {
                    BinWin BinWin = this;
                    if (this.binFile == null)
                    {
                        base.Close();
                    }
                    else
                    {
                        this.textBlockType.Visibility = Visibility.Hidden;
                        TreeViewItem treeViewItem = new TreeViewItem
                        {
                            IsExpanded = true,
                            Tag = this.binFile
                        };
                        treeViewItem.Header = new TextBlock
                        {
                            Text = this.binFile.FilePath.Split(new char[]
                            {
                                '\\'
                            }).Last<string>(),
                            FontSize = 17.0,
                            FontWeight = FontWeights.Bold
                        };
                        this.treeViewProps.Items.Add(treeViewItem);
                        int num = this.binFile.Entries.Count - 1;
                        for (int i = 0; i <= num; i++)
                        {
                            BinFileEntry binFileEntry = this.binFile.Entries[i];
                            TreeViewItem treeViewItem2 = new TreeViewItem
                            {
                                IsExpanded = true,
                                Tag = binFileEntry
                            };
                            StackPanel stackPanel = new StackPanel
                            {
                                Orientation = Orientation.Horizontal
                            };
                            if (binFileEntry.Prop != null)
                            {
                                string str = Convert.ToString(binFileEntry.Prop.Hash);
                                if (binFileEntry.Prop.Prop != null)
                                {
                                    str = binFileEntry.Prop.Prop;
                                }
                                stackPanel.Children.Add(new TextBlock
                                {
                                    Text = "Entry " + str,
                                    FontSize = 16.0,
                                    FontWeight = FontWeights.Medium,
                                    Foreground = new SolidColorBrush(Colors.Gray)
                                });
                            }
                            treeViewItem2.Header = stackPanel;
                              this.method_6(ref binFileEntry.Values, ref treeViewItem2);
                            treeViewItem.Items.Add(treeViewItem2);
                        }
                    }
                }
            }
        }

        public class BinTable
        {
            public class BinValueType
            {
                public BinFileValueType Type;
                public string DisplayName;
                public BinValueType(BinFileValueType type, string displayName)
                {
                    this.Type = type;
                    this.DisplayName = displayName;
                }
            }
            public List<BinWin.BinTable.BinValueType> Types;
            public BinTable()
            {
                this.Types = new List<BinWin.BinTable.BinValueType>();
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.UShortsVector3, "UShort 3-Vector"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.BooleanValue, "Boolean"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.ByteValue2, "Byte"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.const_3, "UInteger Type 1"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.const_4, "UInteger Type 2"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.FloatValue2, "Float"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.FloatsVector2, "Float 2-Vector"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.FloatsVector3, "Float 3-Vector"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.FloatsVector4, "Float 4-Vector"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.ByteVector4_1, "Byte 4-Vector Type 1"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.StringValue, "String"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.const_11, "UInteger Type 3"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.SameTypeValuesList1, "List Type 1"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.ValuesList2, "List Type 2"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.ValuesList, "List Type 3"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.ByteVector4_2, "Byte 4-Vector Type 2"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.SameTypeValuesList2, "List Type 4"));
                this.Types.Add(new BinWin.BinTable.BinValueType(BinFileValueType.DoubleTypesValuesList, "List Type 5"));
            }
            public BinWin.BinTable.BinValueType GetValueType(ref BinFileValueType binfileValueType)
            {
                BinWin.BinTable.BinValueType result;
                try
                {
                    List<BinWin.BinTable.BinValueType>.Enumerator enumerator = this.Types.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        BinWin.BinTable.BinValueType current = enumerator.Current;
                        if (current.Type == binfileValueType)
                        {
                            result = current;
                            return result;
                        }
                    }
                }
                finally
                {

                }
                result = null;
                return result;
            }
        }

        private void SelectChange(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            checked
            {
                if (this.selectedItem.Tag is BinFileValue)
                {
                    try
                    {
                        string arg_3C_1 = this.textBoxValue.Text;
                        TreeViewItem treeViewItem;
                        BinFileValue tag = (BinFileValue)(treeViewItem = this.selectedItem).Tag;
                        this.method_3(arg_3C_1, ref tag);
                        treeViewItem.Tag = tag;
                    }
                    catch (Exception expr_4C)
                    {
                        MessageBox.Show(expr_4C.Message);
                        return;
                    }
                    TreeViewItem treeViewItem2 = this.selectedItem;
                    StackPanel stackPanel = (StackPanel)treeViewItem2.Header;
                    TextBlock textBlock = (TextBlock)stackPanel.Children[stackPanel.Children.Count - 1];
                    textBlock.Text = this.method_2(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(this.selectedItem.Tag, null, "Value", new object[0], null, null, null)));
                }
                else
                {
                    if (this.selectedItem.Tag is BinFileEntry)
                    {
                        BinFileEntry binFileEntry = (BinFileEntry)this.selectedItem.Tag;
                        binFileEntry.Prop = binFileEntry.PropertiesTable.GetProp(Convert.ToUInt32(this.textBoxValue.Text));
                        TreeViewItem treeViewItem3 = this.selectedItem;
                        StackPanel stackPanel2 = (StackPanel)treeViewItem3.Header;
                        string str = Convert.ToString(binFileEntry.Prop.Hash);
                        if (binFileEntry.Prop.Prop != null)
                        {
                            str = binFileEntry.Prop.Prop;
                        }
                        TextBlock textBlock2 = (TextBlock)stackPanel2.Children[stackPanel2.Children.Count - 1];
                        textBlock2.Text = "Entry " + str;
                    }
                }
                this.buttonApply.IsEnabled = false;
            }
        }


        #region 辅助方法
        private string method_0(float float_0)
        {
            return Convert.ToString(float_0, CultureInfo.InvariantCulture.NumberFormat);
        }
        private float method_1(string string_0)
        {
            return Convert.ToSingle(string_0, CultureInfo.InvariantCulture.NumberFormat);
        }
        private string method_2(object object_0)
        {
            string result;
            if (object_0 is float[] | object_0 is byte[] | object_0 is ushort[])
            {
                string text = "[";
                object obj=new object();
                object loopObj = new object();
                if (ObjectFlowControl.ForLoopControl.ForLoopInitObj(obj, 0, Operators.SubtractObject(NewLateBinding.LateGet(object_0, null, "Length", new object[0], null, null, null), 2), 1, ref loopObj, ref obj))
                {
                    do
                    {
                        if (object_0 is float[])
                        {
                            text = text + this.method_0(Convert.ToSingle(NewLateBinding.LateIndexGet(object_0, new object[]
                            {
                                obj
                            }, null))) + ",";
                        }
                        else
                        {
                            text = Convert.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(text, NewLateBinding.LateIndexGet(object_0, new object[]
                            {
                                obj
                            }, null)), ","));
                        }
                    }
                    while (ObjectFlowControl.ForLoopControl.ForNextCheckObj(obj, loopObj, ref obj));
                }
                if (object_0 is float[])
                {
                    text = text + this.method_0(Convert.ToSingle(NewLateBinding.LateIndexGet(object_0, new object[]
                    {
                        Operators.SubtractObject(NewLateBinding.LateGet(object_0, null, "Length", new object[0], null, null, null), 1)
                    }, null))) + "]";
                }
                else
                {
                    text = Convert.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(text, NewLateBinding.LateIndexGet(object_0, new object[]
                    {
                        Operators.SubtractObject(NewLateBinding.LateGet(object_0, null, "Length", new object[0], null, null, null), 1)
                    }, null)), "]"));
                }
                result = text;
            }
            else
            {
                if (object_0 is float)
                {
                    result = this.method_0(Convert.ToSingle(object_0));
                }
                else
                {
                    result = object_0.ToString();
                }
            }
            return result;
        }
        private void method_3(string string_0, ref BinFileValue binFileValue_0)
        {
            if (binFileValue_0.Value is float[] | binFileValue_0.Value is byte[] | binFileValue_0.Value is ushort[])
            {
                string_0 = string_0.Remove(0, 1);
                string_0 = string_0.Remove(checked(string_0.Length - 1), 1);
                string[] array = string_0.Split( ',');
                if (Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(binFileValue_0.Value, null, "Length", new object[0], null, null, null), array.Length, false))
                {
                    throw new Exception("The specified array doesn't have the correct dimension.");
                }
                if (binFileValue_0.Value is float[])
                {
                    object obj = new object();
                    object loopObj = new object();
                    if (ObjectFlowControl.ForLoopControl.ForLoopInitObj(obj, 0, Operators.SubtractObject(NewLateBinding.LateGet(binFileValue_0.Value, null, "Length", new object[0], null, null, null), 1), 1, ref loopObj, ref obj))
                    {
                        do
                        {
                            NewLateBinding.LateIndexSet(binFileValue_0.Value, new object[]
                            {
                                obj,
                                this.method_1(array[Convert.ToInt32(obj)])
                            }, null);
                        }
                        while (ObjectFlowControl.ForLoopControl.ForNextCheckObj(obj, loopObj, ref obj));
                    }
                }
                else
                {
                    object obj2=new object();
                    object loopObj2 = new object();
                    if (ObjectFlowControl.ForLoopControl.ForLoopInitObj(obj2, 0, Operators.SubtractObject(NewLateBinding.LateGet(binFileValue_0.Value, null, "Length", new object[0], null, null, null), 1), 1, ref loopObj2, ref obj2))
                    {
                        do
                        {
                            NewLateBinding.LateIndexSet(binFileValue_0.Value, new object[]
                            {
                                obj2,
                                array[Convert.ToInt32(obj2)]
                            }, null);
                        }
                        while (ObjectFlowControl.ForLoopControl.ForNextCheckObj(obj2, loopObj2, ref obj2));
                    }
                }
            }
            else
            {
                if (binFileValue_0.Value is float)
                {
                    binFileValue_0.Value = this.method_1(string_0);
                }
                else
                {
                    binFileValue_0.Value = string_0;
                }
            }
        }
        private TreeViewItem method_4(ref BinFileValue binFileValue_0)
        {
            TreeViewItem treeViewItem = new TreeViewItem
            {
                Tag = binFileValue_0
            };
            BinFileValueList binFileValueList = (BinFileValueList)binFileValue_0.Value;
            treeViewItem.IsExpanded = true;
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            string str = "";
            if (binFileValue_0.Prop != null)
            {
                str = Convert.ToString(binFileValue_0.Prop.Hash);
                if (binFileValue_0.Prop.Prop != null)
                {
                    str = binFileValue_0.Prop.Prop;
                }
            }
            string str2 = "";
            if (binFileValueList.Prop != null)
            {
                str2 = Convert.ToString(binFileValueList.Prop.Hash);
                if (binFileValueList.Prop.Prop != null)
                {
                    str2 = binFileValueList.Prop.Prop;
                }
            }
            stackPanel.Children.Add(new TextBlock
            {
                Text = str + " List " + str2,
                FontSize = 15.0,
                FontWeight = FontWeights.Medium,
                Foreground = new SolidColorBrush(Colors.Gray)
            });
            treeViewItem.Header = stackPanel;
            this.method_6(ref binFileValueList.Entries, ref treeViewItem);
            return treeViewItem;
        }
        private TreeViewItem method_5(ref BinFileValue binFileValue_0)
        {
            TreeViewItem treeViewItem = new TreeViewItem
            {
                Tag = binFileValue_0
            };
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            if (binFileValue_0.Prop != null)
            {
                string text = Convert.ToString(binFileValue_0.Prop.Hash);
                if (binFileValue_0.Prop.Prop != null)
                {
                    text = binFileValue_0.Prop.Prop;
                }
                stackPanel.Children.Add(new TextBlock
                {
                    Text = text,
                    FontSize = 13.0,
                    FontWeight = FontWeights.Normal,
                    Foreground = new SolidColorBrush(Colors.Gray)
                });
            }
            stackPanel.Children.Add(new TextBlock
            {
                Text = this.method_2(RuntimeHelpers.GetObjectValue(binFileValue_0.Value)),
                FontSize = 13.0,
                FontWeight = FontWeights.Normal,
                Foreground = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(5.0, 0.0, 0.0, 0.0)
            });
            treeViewItem.Header = stackPanel;
            return treeViewItem;
        }
        private void method_6(ref List<BinFileValue> list_0, ref TreeViewItem treeViewItem_0)
        {
            try
            {
                List<BinFileValue>.Enumerator enumerator = list_0.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    BinFileValue current = enumerator.Current;
                    TreeViewItem newItem;
                    if (current.Value is BinFileValueList)
                    {
                        newItem = this.method_4(ref current);
                    }
                    else
                    {
                        newItem = this.method_5(ref current);
                    }
                    treeViewItem_0.Items.Add(newItem);
                }
            }
            finally
            {
            }
        }
        #endregion

    }
}
