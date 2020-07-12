# awk笔记
awk用来处理文本记录，能够实现对文本逐行过滤处理输出。

## 命令语法

```Bash
awk '{pattern + action}' filepath`
```

无论实际使用中的处理多么复杂，awk的命令最终都可以简化为以上的形式来表示，pattern表示在记录中逐行查找过滤的模式，使用正则表达式表示，通过给定的模式忽略掉不符合模式的记录行。action表示对通过过滤模式的记录要进行的处理动作，最简单的处理动作就是使用`print $0`将记录输出。其中的花括号{}用于对指令进行分组，通过分组可以让处理流程条理清晰，形成流水线一样的过程。`fllepath`就是将要处理的输入文件，当然也可以不使用文件输入的形式，这部分在下面会再提及。

## awk命令解析
假设有一份log文件debug.log，部分内容如下：
```
...
2020-02-20 00:00:00.100,debug,process1,no data found according to given criteria
2020-02-20 00:00:01.200,debug,process2,initializing execution context
2020-02-20 00:00:02.300,error,process3,variable is undefined
2020-02-20 00:00:03.400,debug,process1,totally find 365 entries
2020-02-20 00:00:04.500,error,process2,invalid user id!
2020-02-20 00:00:05.600,debug,process1,return code 4
2020-02-20 00:00:06.700,debug,process2,execution context destroyed
...
```
1. 先来看一条简单的命令
```Bash
[robot@server]# awk '{print $0}' /home/robot/debug.log
...
2020-02-20 00:00:00.100,debug,process1,no data found according to given criteria
2020-02-20 00:00:01.200,debug,process2,initializing execution context
2020-02-20 00:00:02.300,error,process3,variable is undefined
2020-02-20 00:00:03.400,debug,process1,totally find 365 entries
2020-02-20 00:00:04.500,error,process2,invalid user id!
2020-02-20 00:00:05.600,debug,process1,return code 4
2020-02-20 00:00:06.700,debug,process2,execution context destroyed
...
```
上述是一条最简单的awk命令，它将debug.log中所有行简单地输出。上述命令中，`/home/robot/debug.log`作为输入文件，awk逐行读取其中地记录，没有使用任何过滤模式，对每条记录，调用print输出$0的内容，这里的$0是awk中的变量形式，以$符号开头跟变量名，$0表示当前正在处理的记录内容，也就是每一行的文件内容。

2. 另外两种形式
```Bash
[robot@server]# cat /home/robot/debug.log | awk '{print $0}'
...
2020-02-20 00:00:00.100,debug,process1,no data found according to given criteria
2020-02-20 00:00:01.200,debug,process2,initializing execution context
2020-02-20 00:00:02.300,error,process3,variable is undefined
2020-02-20 00:00:03.400,debug,process1,totally find 365 entries
2020-02-20 00:00:04.500,error,process2,invalid user id!
2020-02-20 00:00:05.600,debug,process1,return code 4
2020-02-20 00:00:06.700,debug,process2,execution context destroyed
...
[robot@server]# echo 'hello world' | awk '{print $0}'
hello world
[robot@server]# awk '{print "pass"' /home/robot/debug.log
...
pass
pass
pass
pass
pass
pass
pass
...
```
上述第一条命令通过cat输出文件内容并重定向输出到awk命令，其实跟直接用awk并指定文件路径效果一样，第二条命令将echo的内容重定向给awk命令，这只是一个简单使用，用于说明awk可用于组合管道重定向实现流水线处理，第三条命令对于每一行记录不再输出当前行内容，而是输出指定的内容，这里是固定的字符串pass，当然也可以是任意组合处理的字符串。
