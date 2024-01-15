
/* A Bison parser, made by GNU Bison 2.4.1.  */

/* Skeleton interface for Bison's Yacc-like parsers in C
   
      Copyright (C) 1984, 1989, 1990, 2000, 2001, 2002, 2003, 2004, 2005, 2006
   Free Software Foundation, Inc.
   
   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.
   
   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.
   
   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.  */

/* As a special exception, you may create a larger work that contains
   part or all of the Bison parser skeleton and distribute that work
   under terms of your choice, so long as that work isn't itself a
   parser generator using the skeleton or a modified version thereof
   as a parser skeleton.  Alternatively, if you modify or redistribute
   the parser skeleton itself, you may (at your option) remove this
   special exception, which will cause the skeleton and the resulting
   Bison output files to be licensed under the GNU General Public
   License without this special exception.
   
   This special exception was added by the Free Software Foundation in
   version 2.2 of Bison.  */


/* Tokens.  */
#ifndef YYTOKENTYPE
# define YYTOKENTYPE
   /* Put the tokens into the symbol table, so that GDB and other debuggers
      know about them.  */
   enum yytokentype {
     INT = 258,
     STRING = 259,
     BOOL = 260,
     READ = 261,
     WRITE = 262,
     IF = 263,
     ELSE = 264,
     WHILE = 265,
     RETURN = 266,
     FUNCTION = 267,
     LIST = 268,
     TRUE = 269,
     FALSE = 270,
     AND = 271,
     OR = 272,
     plus = 273,
     minus = 274,
     mul = 275,
     division = 276,
     mod = 277,
     lessOrEqual = 278,
     moreOrEqual = 279,
     less = 280,
     more = 281,
     equal = 282,
     different = 283,
     eq = 284,
     leftCurlyBracket = 285,
     rightCurlyBracket = 286,
     leftRoundBracket = 287,
     rightRoundBracket = 288,
     leftBracket = 289,
     rightBracket = 290,
     colon = 291,
     semicolon = 292,
     comma = 293,
     apostrophe = 294,
     quote = 295,
     IDENTIFIER = 296,
     NUMBER_CONST = 297,
     STRING_CONST = 298,
     CHAR_CONST = 299
   };
#endif



#if ! defined YYSTYPE && ! defined YYSTYPE_IS_DECLARED
typedef int YYSTYPE;
# define YYSTYPE_IS_TRIVIAL 1
# define yystype YYSTYPE /* obsolescent; will be withdrawn */
# define YYSTYPE_IS_DECLARED 1
#endif

extern YYSTYPE yylval;


