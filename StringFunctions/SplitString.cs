using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;

namespace StringFunctions
{
    public class SplitString : CodeActivity
    {
        public enum SplitOptions
        {
            Comma,
            Newline,
            Pipe,
            Custom
        }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Separator")]
        public SplitOptions Splitter { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Input { get; set; }

        [Category("Input")]
        [DisplayName("Separators (If Custom)")]
        public InArgument<string[]> CustomSplitter { get; set; }

        [Category("Output")]
        [RequiredArgument]
        public OutArgument<string[]> Result { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                SplitOptions splitter = Splitter;
                List<string> temp = new List<string>();
                String[] stringsplitter;
                string input;
                switch (splitter)
                {
                    case SplitOptions.Comma:
                        input = Input.Get(context);
                        stringsplitter = new String[] { "," };
                        var tmp = input.Split(stringsplitter, StringSplitOptions.RemoveEmptyEntries);
                        temp = tmp.ToList<string>();
                        break;
                    case SplitOptions.Newline:
                        input = Input.Get(context);
                        stringsplitter = new String[] { Environment.NewLine.ToString() };
                        tmp = input.Split(stringsplitter, StringSplitOptions.RemoveEmptyEntries);
                        temp = tmp.ToList<string>();
                        break;
                    case SplitOptions.Pipe:
                        input = Input.Get(context);
                        stringsplitter = new String[] {"|"};
                        tmp = input.Split(stringsplitter, StringSplitOptions.RemoveEmptyEntries);
                        temp = tmp.ToList<string>();
                        break;
                    case SplitOptions.Custom:
                        input = Input.Get(context);
                        string[] customSplitter = CustomSplitter.Get(context);
                        if (customSplitter != null)
                        {
                            tmp = input.Split(customSplitter, StringSplitOptions.RemoveEmptyEntries);
                            temp = tmp.ToList<string>();
                            break;
                        }
                        else
                        {
                            throw new NullReferenceException("Custom splitter cannot be null if Custom is selected");
                        }
                        
                    default:
                        Console.WriteLine("Invalid Splitter");
                        break;
                }

                Result.Set(context, temp.ToArray());
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
