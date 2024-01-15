%{
#include <stdio.h>
#include <stdlib.h>

#define YYDEBUG 1 
%}

%token INT
%token STRING
%token BOOL
%token READ
%token WRITE
%token IF
%token ELSE
%token WHILE
%token RETURN
%token FUNCTION
%token LIST
%token TRUE
%token FALSE
%token AND
%token OR

%token plus
%token minus
%token mul
%token division
%token mod
%token lessOrEqual
%token moreOrEqual
%token less
%token more
%token equal
%token different
%token eq



%token leftCurlyBracket
%token rightCurlyBracket
%token leftRoundBracket
%token rightRoundBracket
%token leftBracket
%token rightBracket
%token colon
%token semicolon
%token comma
%token apostrophe
%token quote

%token IDENTIFIER
%token NUMBER_CONST
%token STRING_CONST
%token CHAR_CONST

%start function

%%

function : FUNCTION compound_statement

statement : declaration semicolon | assignment_statement | return_statement semicolon | iostmt semicolon | if_statement | while_statement

statement_list : statement | statement statement_list

compound_statement : leftCurlyBracket statement_list rightCurlyBracket

expression : expression plus term | expression minus term | term

term : term mul factor | term division factor | term mod factor | factor 

factor : leftRoundBracket expression rightRoundBracket | IDENTIFIER | constant | bool_values | LIST | list_element

constant : NUMBER_CONST | STRING_CONST | CHAR_CONST 

iostmt : READ leftRoundBracket IDENTIFIER rightRoundBracket | WRITE leftRoundBracket IDENTIFIER rightRoundBracket | WRITE leftRoundBracket constant rightRoundBracket

simple_type : INT | STRING | BOOL

array_declaration : LIST simple_type IDENTIFIER leftBracket rightBracket

declaration : simple_type IDENTIFIER | array_declaration 

assignment_statement : simple_type IDENTIFIER eq expression semicolon | IDENTIFIER eq expression semicolon

if_statement : IF leftRoundBracket condition_list rightRoundBracket compound_statement | IF leftRoundBracket condition_list rightRoundBracket compound_statement ELSE compound_statement

while_statement : WHILE leftRoundBracket condition rightRoundBracket compound_statement

return_statement : RETURN expression 

condition : expression relation expression

condition_list : condition | condition comparison_values condition_list

relation : less | lessOrEqual | equal | different | moreOrEqual | more

bool_values : TRUE | FALSE

comparison_values : AND | OR

list_element : IDENTIFIER leftBracket IDENTIFIER rightBracket

%%

yyerror(char *s)
{	
	printf("%s\n",s);
}

extern FILE *yyin;

main(int argc, char **argv)
{
	if(argc>1) yyin = fopen(argv[1],"r");
	if(argc>2 && !strcmp(argv[2],"-d")) yydebug = 1;
	if(!yyparse()) fprintf(stderr, "\tProgram is syntactically correct.\n");
	return 0;
}