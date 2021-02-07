ö
u/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/DataOperator/Context/BaseDbContext.cs
	namespace 	
DataOperator
 
. 
Context 
{		 
public

 

class

 
BaseDbContext

 
:

  
	DbContext

! *
{ 
public 
BaseDbContext 
( 
DbContextOptions -
<- .
BaseDbContext. ;
>; <
options= D
)D E
: 
base 
( 
options 
) 
{ 
} 
public 
DbSet 
< 
ReportConfiguration (
>( ) 
ReportConfigurations* >
{? @
getA D
;D E
setF I
;I J
}K L
public 
DbSet 
< 
Logs 
> 
logs 
{  !
get" %
;% &
set' *
;* +
}, -
	protected 
override 
void 
OnModelCreating  /
(/ 0
ModelBuilder0 <
modelBuilder= I
)I J
{ 	
modelBuilder 
. +
ApplyConfigurationsFromAssembly 8
(8 9
Assembly9 A
.A B 
GetExecutingAssemblyB V
(V W
)W X
)X Y
;Y Z
} 	
} 
} ¦ 
t/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/DataOperator/Context/ISqlExecuter.cs
	namespace 	
DataOperator
 
. 
Context 
{ 
public		 

	interface		 
ISqlExecuter		 !
{

 
	DataTable 
GetSqlFeedback  
(  !
string! '
	SqlString( 1
,1 2
DateTime3 ;
?; <
DateParameter= J
)J K
;K L
} 
public 

class 
SqlExecuter 
: 
ISqlExecuter +
{ 
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
private 
readonly 
ILogger  
<  !
SqlExecuter! ,
>, -
_logger. 5
;5 6
public 
SqlExecuter 
( 
IConfiguration )
configuration* 7
,7 8
ILogger9 @
<@ A
SqlExecuterA L
>L M
loggerN T
)T U
{ 	
Configuration 
= 
configuration )
;) *
_logger 
= 
logger 
; 
} 	
public 
	DataTable 
GetSqlFeedback '
(' (
string( .

SqlCommand/ 9
,9 :
DateTime; C
?C D
DateParameterE R
)R S
{ 	
var 
connectionString  
=! "
Configuration# 0
[0 1
$str1 V
]V W
;W X
using 
( 
SqlConnection  

connection! +
=, -
new. 1
SqlConnection2 ?
(? @
connectionString@ P
)P Q
)Q R
{ 
var   
queryString   
=    !

SqlCommand  " ,
;  , -
using"" 
("" 
var"" 
dataAdapter"" &
=""' (
new"") ,
SqlDataAdapter""- ;
(""; <
)""< =
)""= >
{## 
var$$ 
dataFromSql$$ #
=$$$ %
new$$& )
	DataTable$$* 3
($$3 4
)$$4 5
;$$5 6
dataAdapter&& 
.&&  
SelectCommand&&  -
=&&. /
new&&0 3

SqlCommand&&4 >
(&&> ?

SqlCommand&&? I
,&&I J

connection&&K U
)&&U V
;&&V W
dataAdapter(( 
.((  
SelectCommand((  -
.((- .

Parameters((. 8
.((8 9
Add((9 <
(((< =
$str((= D
,((D E
	SqlDbType((F O
.((O P
Date((P T
)((T U
;((U V
dataAdapter** 
.**  
SelectCommand**  -
.**- .

Parameters**. 8
[**8 9
$str**9 @
]**@ A
.**A B
Value**B G
=**H I
DateParameter**J W
;**W X
_logger,, 
.,, 
LogInformation,, *
(,,* +
dataAdapter,,+ 6
.,,6 7
SelectCommand,,7 D
.,,D E
CommandText,,E P
),,P Q
;,,Q R
try.. 
{// 

connection00 "
.00" #
Open00# '
(00' (
)00( )
;00) *
dataAdapter22 #
.22# $
Fill22$ (
(22( )
dataFromSql22) 4
)224 5
;225 6
return44 
dataFromSql44 *
;44* +
}55 
catch66 
(66 
	Exception66 $
ex66% '
)66' (
{77 
_logger88 
.88  
LogInformation88  .
(88. /
ex88/ 1
.881 2
Message882 9
)889 :
;88: ;
return:: 
null:: #
;::# $
};; 
}<< 
}== 
}>> 	
}AA 
}BB ‡
k/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/DataOperator/Models/Logs.cs
	namespace

 	
DataOperator


 
.

 
Models

 
{ 
[ 
Table 

(
 
$str 
, 
Schema #
=$ %
$str& /
)/ 0
]0 1
public 

class 
Logs 
{ 
public 
int 
	ServiceId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 

LogMessage  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
ServiceMethod #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
DateTimeOffset 
LogTime %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
Logs 
( 
) 
{ 
} 
public 
Logs 
( 
string 
	logString $
,$ %
int& )
	serviceId* 3
,3 4
string5 ;
serviceMethod< I
)I J
{ 	
	ServiceId 
= 
	serviceId !
;! "

LogMessage 
= 
	logString "
;" #
ServiceMethod 
= 
serviceMethod )
;) *
LogTime 
= 
DateTimeOffset $
.$ %
Now% (
;( )
} 	
} 
public 

class !
ApplyToDbContext_LOGS &
:' ($
IEntityTypeConfiguration) A
<A B
LogsB F
>F G
{   
public!! 
virtual!! 
void!! 
	Configure!! %
(!!% &
EntityTypeBuilder!!& 7
<!!7 8
Logs!!8 <
>!!< =
builder!!> E
)!!E F
{"" 	
builder## 
.## 
HasKey## 
(## 
x## 
=>##  
new##! $
{##% &
x##' (
.##( )

LogMessage##) 3
,##3 4
x##5 6
.##6 7
LogTime##7 >
,##> ?
x##@ A
.##A B
	ServiceId##B K
,##K L
x##M N
.##N O
ServiceMethod##O \
}##\ ]
)##] ^
;##^ _
}$$ 	
}%% 
}&& ²
v/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/DataOperator/Models/ReportingModels.cs
	namespace 	
DataOperator
 
. 
Models 
{ 
[		 
Table		 

(		
 
$str		 
,		 
Schema		  &
=		' (
$str		) 4
)		4 5
]		5 6
public

 

class

 
ReportConfiguration

 $
{ 
public 
string 
Report_Name !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 

Report_Sql  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
Report_Separator &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
	Parameter 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 

Header_Row  
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
public 

class 0
$ApplyToDbContext_ReportConfiguration 5
:6 7$
IEntityTypeConfiguration8 P
<P Q
ReportConfigurationQ d
>d e
{ 
public 
virtual 
void 
	Configure %
(% &
EntityTypeBuilder& 7
<7 8
ReportConfiguration8 K
>K L
builderM T
)T U
{ 	
builder 
. 
HasKey 
( 
x 
=> 
x  !
.! "
Report_Name" -
)- .
;. /
} 	
} 
}"" 