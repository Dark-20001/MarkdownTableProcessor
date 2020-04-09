using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;

/*
 * Harry Tian
 * 2020/4/8
 * use HTML Comments structre <!-- -->
 * to protect not translatable text in markdown *.md file
 * out file in *.mdh format
 * 
 * rules:
| 回调 | 功能 |
| 方法 | 功能 |

| 枚举值 | 含义 |
| 参数 | 含义 |

| 参数 | 类型 | 取值 |
| 参数 | 类型 | 描述 |

| Public 回调函数 | 函数名 |
| 函数 | 函数名 |

| 错误码                            | 含义                                                         |

| public 成员函数 | 方法签名                                                     |
| public 回调函数 | 方法签名                                                     |
|   public 静态函数 | 方法签名                                                     |

| 全局函数 | 函数名 |
| 功能                             | 旧接口                                      | 新接口                                |

| 接口 | 变化 |
| 参数 | 描述 |
 */

namespace MarkdownTableProcessor
{
    public class Processor
    {
        public enum mode
        {
            m0,
            m12_1,
            m12_2,
            m12_12,
            m123_12,
            m123_23,
            m1234_1,
            m1234_12
        }

        Regex regex12 = new Regex(@"^\|(.*?)\|(.*?)\|$", RegexOptions.Compiled);
        string repl12_1 = @"|<!--$1-->|$2|";
        string repl12_2 = @"|$1|<!--$2-->|";
        string repl12_12 = @"|<!--$1-->|<!--$2-->|";
        Regex regex123 = new Regex(@"^\|(.*?)\|(.*?)\|(.*?)\|$", RegexOptions.Compiled);
        string repl123_12 = @"|<!--$1-->|<!--$2-->|$3|";
        string repl123_23 = @"|$1|<!--$2-->|<!--$3-->|";
        Regex regex1234 = new Regex(@"^\|(.*?)\|(.*?)\|(.*?)\|(.*?)\|$", RegexOptions.Compiled);
        string repl1234_1 = @"|<!--$1-->|$2|$3|$4|";
        string repl1234_12 = @"|<!--$1-->|<!--$2-->|$3|$4|";

        Regex regHeader = new Regex(@"^[-\s:\|]+$", RegexOptions.Compiled);     //@"^\|[-\s:\|]+\|$"
        string headerRepl = "-->|<!--";

        public void Procress(string f)
        {
            Regex regex_a = new Regex(@"^\|\s+(回调|方法)\s+\|\s+功能\s+\|$", RegexOptions.Compiled);  //12_1
            Regex regex_b = new Regex(@"^\|\s+(枚举值|参数)\s+\|\s+含义\s+\|$", RegexOptions.Compiled);  //12_1

            Regex regex_c = new Regex(@"^\|\s+(.*?)函数\s+\|\s+(函数名|方法签名)\s+\|$", RegexOptions.Compiled); //12_12

            Regex regex_d = new Regex(@"^\|\s+接口\s+\|\s+变化\s+\|$", RegexOptions.Compiled); //12_1
            Regex regex_e = new Regex(@"^\|\s+错误码\s+\|\s+(含义|错误描述)\s+\|$", RegexOptions.Compiled);  //12_1
            Regex regex_f = new Regex(@"^\|\s+(参数|名称)\s+\|\s+描述\s+\|$", RegexOptions.Compiled);  //12_1

            Regex regex_w = new Regex(@"^\|\s+功能\s+\|\s+旧接口\s+\|\s+新接口\s+\|$", RegexOptions.Compiled); //123_23
            Regex regex_x = new Regex(@"^\|\s+参数\s+\|\s+类型\s+\|\s+(取值|描述)\s+\|$", RegexOptions.Compiled); //123_12
            Regex regex_y = new Regex(@"^\|\s+名称\s+\|\s+(是否必需|数据类型)\s+\|\s+(数据类型|是否必需)\s+\|\s+描述\s+\|$", RegexOptions.Compiled); //1234_1
            Regex regex_z = new Regex(@"^.*\|\s+名称\s+\|\s+类型\s+\|\s+是否必选\s+\|\s+示例值\s+\|\s+描述\s+\|$\n", RegexOptions.Compiled); //1234_12

            string o = f + "h";
            if (File.Exists(o))
                File.Delete(o);
            UTF8Encoding utf8 = new UTF8Encoding(false);
            StreamReader sr = new StreamReader(f, Encoding.UTF8, true);
            StreamWriter sw = new StreamWriter(o, false, utf8);
            sw.AutoFlush = true;

            string line = string.Empty;
            bool tableStart = false;
            mode m = mode.m0;
            bool matched = false;

            while (sr.Peek() > -1)
            {
                line = sr.ReadLine();

                if (line.Trim().StartsWith("|") && line.Trim().EndsWith("|"))
                {
                    if (!tableStart)
                    {
                        tableStart = true;
                        sw.WriteLine(line);

                        if (!matched)
                        {
                            if (regex_a.IsMatch(line))
                            {
                                m = mode.m12_1;
                                matched = true;
                            }
                        }
                        if (!matched)
                        {
                            if (regex_b.IsMatch(line))
                            {
                                m = mode.m12_1;
                                matched = true;
                            }
                        }
                        if (!matched)
                        {
                            if (regex_c.IsMatch(line))
                            {
                                m = mode.m12_12;
                                matched = true;
                            }
                        }
                        if (!matched)
                        {
                            if (regex_d.IsMatch(line))
                            {
                                m = mode.m12_1;
                                matched = true;
                            }
                        }
                        if (!matched)
                        {
                            if (regex_e.IsMatch(line))
                            {
                                m = mode.m12_1;
                                matched = true;
                            }
                        }
                        if (!matched)
                        {
                            if (regex_f.IsMatch(line))
                            {
                                m = mode.m12_1;
                                matched = true;
                            }
                        }
                        if (!matched)
                        {
                            if (regex_w.IsMatch(line))
                            {
                                m = mode.m123_23;
                                matched = true;
                            }
                        }
                        if (!matched)
                        {
                            if (regex_x.IsMatch(line))
                            {
                                m = mode.m123_12;
                                matched = true;
                            }
                        }
                        if (!matched)
                        {
                            if (regex_y.IsMatch(line))
                            {
                                m = mode.m1234_1;
                                matched = true;
                            }
                        }
                        if (!matched)
                        {
                            if (regex_z.IsMatch(line))
                            {
                                m = mode.m1234_12;
                                matched = true;
                            }
                        }



                        line = sr.ReadLine();
                        if (regHeader.IsMatch(line))
                        {
                            //line = regHeader.Replace(@"\|", headerRepl);
                            //line = line.Substring(3, line.Length - 7);
                            //line = "<!--" + line + "-->";
                            sw.WriteLine(line);
                        }
                        else
                        {
                            line = ProcressLine(line, m);
                            sw.WriteLine(line);
                        }
                    }
                    else
                    {
                        line = ProcressLine(line, m);
                        sw.WriteLine(line);
                    }
                }
                else
                {
                    tableStart = false;
                    m = mode.m0;
                    matched = false;
                    sw.WriteLine(line);
                }
            }


            if (sr != null)
                sr.Close();
            if (sw != null)
                sw.Close();
        }

        private string ProcressLine(string line, mode m)
        {
            switch (m)
            {
                case mode.m0:
                    return line;
                case mode.m12_1:
                    return ProcressLine12_1(line);
                case mode.m12_2:
                    return ProcressLine12_2(line);
                case mode.m12_12:
                    return ProcressLine12_12(line);
                case mode.m123_12:
                    return ProcressLine123_12(line);
                case mode.m123_23:
                    return ProcressLine123_23(line);
                case mode.m1234_1:
                    return ProcressLine1234_1(line);
                case mode.m1234_12:
                    return ProcressLine1234_12(line);
                default:
                    return line;
            }
        }

        private string ProcressLine12_1(string line)
        {
            line = regex12.Replace(line.Trim(), repl12_1);
            return line;
        }

        private string ProcressLine12_2(string line)
        {
            line = regex12.Replace(line.Trim(), repl12_2);
            return line;
        }

        private string ProcressLine12_12(string line)
        {
            line = regex12.Replace(line.Trim(), repl12_12);
            return line;
        }

        private string ProcressLine123_12(string line)
        {
            line = regex123.Replace(line.Trim(), repl123_12);
            return line;
        }
        private string ProcressLine123_23(string line)
        {
            line = regex123.Replace(line.Trim(), repl123_23);
            return line;
        }
        private string ProcressLine1234_1(string line)
        {
            line = regex1234.Replace(line.Trim(), repl1234_1);
            return line;
        }
        private string ProcressLine1234_12(string line)
        {
            line = regex1234.Replace(line.Trim(), repl1234_12);
            return line;
        }


        public void Post(string f)
        {
            string o = f + "h";
            if (File.Exists(o))
                File.Delete(o);
            UTF8Encoding utf8 = new UTF8Encoding(false);
            StreamReader sr = new StreamReader(f, Encoding.UTF8, true);
            StreamWriter sw = new StreamWriter(o, false, utf8);
            sw.AutoFlush = true;

            string line = string.Empty;
            bool tableStart = false;


            while (sr.Peek() > -1)
            {
                line = sr.ReadLine();

                if (line.Trim().StartsWith("|"))
                {
                    if (!tableStart)
                    {
                        tableStart = true;
                        sw.WriteLine(line+"|");

                        line = sr.ReadLine();
                        if (regHeader.IsMatch(line))
                        {
                            //line = regHeader.Replace(@"\|", headerRepl);
                            //line = line.Substring(3, line.Length - 7);
                            //line = "<!--" + line + "-->";
                            sw.WriteLine(line+"|");
                        }
                        else
                        {
                            line = line.Replace("<!--", "");
                            line = line.Replace("-->", "");
                            sw.WriteLine(line+"|");
                        }
                    }
                    else
                    {
                        line = line.Replace("<!--", "");
                        line = line.Replace("-->", "");
                        sw.WriteLine(line+"|");
                    }
                }
                else
                {
                    tableStart = false;
                    sw.WriteLine(line);
                }

            }
        }

        public void FixComment(string f)
        {
            string o = f + "h";
            if (File.Exists(o))
                File.Delete(o);
            UTF8Encoding utf8 = new UTF8Encoding(false);
            StreamReader sr = new StreamReader(f, Encoding.UTF8, true);
            StreamWriter sw = new StreamWriter(o, false, utf8);
            sw.AutoFlush = true;

            string line = string.Empty;
            bool CommentStart = false;
            while (sr.Peek() > -1)
            {
                line = sr.ReadLine();
                if (line.TrimStart().StartsWith("/*"))
                {
                    if (!CommentStart)
                    {
                        CommentStart = true;
                        sw.WriteLine(line);
                    }
                    else
                    {
                        if (!line.TrimStart().StartsWith("*"))
                        {
                            sw.WriteLine("* " + line);
                        }
                        else
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                else if (line.Contains("*/"))
                {
                    CommentStart = false;
                    sw.WriteLine(line);
                }
                else
                {
                    if (CommentStart)
                    {
                        if (!line.TrimStart().StartsWith("*"))
                        {
                            sw.WriteLine("* " + line);
                        }
                        else
                        {
                            sw.WriteLine(line);
                        }
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                    
                }
            }
        }
    }
}
