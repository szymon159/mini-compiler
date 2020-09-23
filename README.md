# Mini Compiler
Compiler of  simple programming language called "MINI" designed with Gardens Point tools. Created for Computer Science classes at Warsaw University og Technology.

## Language
MINI language is a simple language using a few ideas and syntax similar to what was originally introduced in C. Source code cannot be divided into multiple methods - the one only supported method is _program_ which contains all the code of the program. All supported data types are: bool, int and double. Grammar for the lanugage can be found in the file _kompilator.report.html_

- Supported keywords: program if else while read write return int double bool true false
- Supported operators: = || && | & == != > >= < <= + - * / ! ~ ( ) { } ;

## About
The input of the program is a code in "MINI" language and the output is the _*.il_ file with CIL code which can be then made into executable using ILAsm tools. 

In order to make changes in parser or scanner code, you need to modify _kompilator.lex_ or _kompilator.y_ files respectively and run a script _gp.bat_ included in subdirectory _lib/gp_ to generate actual .NET Framework compatible code.

## Dependencies
Project references library _QUT.ShiftReduceParser_ for shift-reduce conflicts in GPPG parsers.

It also utilizes _Gppg.exe_ and _Gplex.exe_ binaries which are to be found in directory _lib/gp_ for generating Parser.cs and Scanner.cs files. All those tools are created by Gardens Point and are only used in this project for educational purposes.

## Technology
- .NET Framework 4.7.2
