# MarkdownTableProcessor


Harry Tian
2020/4/9
This tool works with SDL Trados build-in Markdown Parser.
Added function to fix missing end | for table after generate target file.

2020/4/8
use HTML Comments structre <!-- -->
to protect not translatable text in markdown *.md file
out file in *.mdh format


rules:
```
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
```
