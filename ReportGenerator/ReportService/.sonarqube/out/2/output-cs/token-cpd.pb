•
Ü/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/ReportService/ConfigurationOptions/AzureQueueConfig.cs
	namespace 	
ReportService
 
. 
ConfigurationLogic *
{ 
public 

class 
AzureQueueConfig !
{ 
public 
AzureQueueConfig 
(  
)  !
{ 	
} 	
public

 
const

 
string

 #
AzureQueueConfigSection

 3
=

4 5
$str

6 H
;

H I
public 
string 
StorageConnection '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
	QueueName 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} ç/
Ç/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/ReportService/Migrations/20210131191004_Initial.cs
	namespace 	
ReportService
 
. 

Migrations "
{ 
public 

partial 
class 
Initial  
:! "
	Migration# ,
{ 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{		 	
migrationBuilder

 
.

 
EnsureSchema

 )
(

) *
name 
: 
$str 
)  
;  !
migrationBuilder 
. 
EnsureSchema )
() *
name 
: 
$str !
)! "
;" #
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str &
,& '
schema 
: 
$str !
,! "
columns 
: 
table 
=> !
new" %
{ 
	ServiceId 
= 
table  %
.% &
Column& ,
<, -
int- 0
>0 1
(1 2
type2 6
:6 7
$str8 =
,= >
nullable? G
:G H
falseI N
)N O
,O P

LogMessage 
=  
table! &
.& '
Column' -
<- .
string. 4
>4 5
(5 6
type6 :
:: ;
$str< K
,K L
nullableM U
:U V
falseW \
)\ ]
,] ^
ServiceMethod !
=" #
table$ )
.) *
Column* 0
<0 1
string1 7
>7 8
(8 9
type9 =
:= >
$str? N
,N O
nullableP X
:X Y
falseZ _
)_ `
,` a
LogTime 
= 
table #
.# $
Column$ *
<* +
DateTimeOffset+ 9
>9 :
(: ;
type; ?
:? @
$strA Q
,Q R
nullableS [
:[ \
false] b
)b c
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 8
,8 9
x: ;
=>< >
new? B
{C D
xE F
.F G

LogMessageG Q
,Q R
xS T
.T U
LogTimeU \
,\ ]
x^ _
._ `
	ServiceId` i
,i j
xk l
.l m
ServiceMethodm z
}{ |
)| }
;} ~
} 
) 
; 
migrationBuilder 
. 
CreateTable (
(( )
name   
:   
$str   )
,  ) *
schema!! 
:!! 
$str!! #
,!!# $
columns"" 
:"" 
table"" 
=>"" !
new""" %
{## 
Report_Name$$ 
=$$  !
table$$" '
.$$' (
Column$$( .
<$$. /
string$$/ 5
>$$5 6
($$6 7
type$$7 ;
:$$; <
$str$$= L
,$$L M
nullable$$N V
:$$V W
false$$X ]
)$$] ^
,$$^ _

Report_Sql%% 
=%%  
table%%! &
.%%& '
Column%%' -
<%%- .
string%%. 4
>%%4 5
(%%5 6
type%%6 :
:%%: ;
$str%%< K
,%%K L
nullable%%M U
:%%U V
true%%W [
)%%[ \
,%%\ ]
Report_Separator&& $
=&&% &
table&&' ,
.&&, -
Column&&- 3
<&&3 4
string&&4 :
>&&: ;
(&&; <
type&&< @
:&&@ A
$str&&B Q
,&&Q R
nullable&&S [
:&&[ \
true&&] a
)&&a b
,&&b c
	Parameter'' 
='' 
table''  %
.''% &
Column''& ,
<'', -
string''- 3
>''3 4
(''4 5
type''5 9
:''9 :
$str''; J
,''J K
nullable''L T
:''T U
true''V Z
)''Z [
,''[ \

Header_Row(( 
=((  
table((! &
.((& '
Column((' -
<((- .
string((. 4
>((4 5
(((5 6
type((6 :
:((: ;
$str((< K
,((K L
nullable((M U
:((U V
true((W [
)(([ \
})) 
,)) 
constraints** 
:** 
table** "
=>**# %
{++ 
table,, 
.,, 

PrimaryKey,, $
(,,$ %
$str,,% ;
,,,; <
x,,= >
=>,,? A
x,,B C
.,,C D
Report_Name,,D O
),,O P
;,,P Q
}-- 
)-- 
;-- 
}.. 	
	protected00 
override00 
void00 
Down00  $
(00$ %
MigrationBuilder00% 5
migrationBuilder006 F
)00F G
{11 	
migrationBuilder22 
.22 
	DropTable22 &
(22& '
name33 
:33 
$str33 &
,33& '
schema44 
:44 
$str44 !
)44! "
;44" #
migrationBuilder66 
.66 
	DropTable66 &
(66& '
name77 
:77 
$str77 )
,77) *
schema88 
:88 
$str88 #
)88# $
;88$ %
}99 	
}:: 
};; Î&
h/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/ReportService/Program.cs
	namespace 	
ReportService
 
{ 
public 

class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	
CreateHostBuilder 
( 
args "
)" #
.# $
Build$ )
() *
)* +
.+ ,
Run, /
(/ 0
)0 1
;1 2
} 	
public 
static 
IHostBuilder "
CreateHostBuilder# 4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
Host 
.  
CreateDefaultBuilder %
(% &
args& *
)* +
. %
ConfigureAppConfiguration &
(& '
(' (
context( /
,/ 0

hostConfig0 :
): ;
=>< >
{ 

hostConfig 
. #
AddEnvironmentVariables 2
(2 3
)3 4
;4 5
Console 
. 
	WriteLine !
(! "
context" )
.) *
HostingEnvironment* <
.< =
EnvironmentName= L
)L M
;M N
if 
( 
context 
. 
HostingEnvironment .
.. /
IsDevelopment/ <
(< =
)= >
)> ?
{   

hostConfig!! 
.!! 
AddUserSecrets!! -
<!!- .
Program!!. 5
>!!5 6
(!!6 7
)!!7 8
;!!8 9
}"" 
}## 
)## 
.$$ 
ConfigureServices$$ "
($$" #
($$# $

hostConfig$$$ .
,$$. /
services$$0 8
)$$8 9
=>$$: <
{%% 
var'' 
configuration'' %
=''& '

hostConfig''( 2
.''2 3
Configuration''3 @
;''@ A
services)) 
.)) 
	Configure)) &
<))& '
AzureQueueConfig))' 7
>))7 8
())8 9
configuration))9 F
.))F G

GetSection))G Q
())Q R
AzureQueueConfig**( 8
.**8 9#
AzureQueueConfigSection**9 P
)**P Q
)**Q R
;**R S
services,, 
.,, 
	Configure,, &
<,,& '!
CloudFileWriterConfig,,' <
>,,< =
(,,= >
configuration,,> K
.,,K L

GetSection,,L V
(,,V W!
CloudFileWriterConfig--( =
.--= >(
CloudFileWriterConfigSection--> Z
)--Z [
)--[ \
;--\ ]
services// 
.// 
	Configure// &
<//& '
FileWriterConfig//' 7
>//7 8
(//8 9
configuration//9 F
.//F G

GetSection//G Q
(//Q R
FileWriterConfig00( 8
.008 9#
FileWriterConfigSection009 P
)00P Q
)00Q R
;00R S
services33 
.33 
AddDbContext33 )
<33) *
BaseDbContext33* 7
>337 8
(338 9
options339 @
=>33A C
options44 
.44 
UseSqlServer44 $
(44$ %
configuration44% 2
[442 3
$str443 X
]44X Y
,44Y Z
a44[ \
=>44] _
a44` a
.44a b
MigrationsAssembly44b t
(44t u
$str	44u Ñ
)
44Ñ Ö
)
44Ö Ü
,
44Ü á
ServiceLifetime55 #
.55# $
	Singleton55$ -
)55- .
;55. /
services99 
.99 
AddHostedService99 -
<99- .
ReportServiceHosted99. A
>99A B
(99B C
)99C D
;99D E
services;; 
.;; 
AddTransient;; )
<;;) *
IFileWriter;;* 5
,;;5 6

FileWriter;;7 A
>;;T U
(;;U V
);;V W
;;;W X
services== 
.== 
AddTransient== )
<==) *
IReportFile==* 5
,==5 6

ReportFile==7 A
>==A B
(==B C
)==C D
;==D E
services?? 
.?? 
AddTransient?? )
<??) *
IReportsQueue??* 7
,??7 8
ReportsQueue??9 E
>??E F
(??F G
)??G H
;??H I
servicesAA 
.AA 
AddTransientAA )
<AA) *
ISqlExecuterAA* 6
,AA6 7
SqlExecuterAA8 C
>AAC D
(AAD E
)AAE F
;AAF G
}BB 
)BB 
;BB 
}DD 
}EE ˚;
z/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/ReportService/QueueService/ReportsQueue.cs
	namespace 	
ReportService
 
. 
QueueService $
{ 
public 

	interface 
IReportsQueue "
{ 
Task 
DequeueMessages 
( 
QueueMessage )
[) *
]* +
messages, 4
,4 5
QueueClient6 A
queueClientB M
)M N
;N O
QueueClient 
CreateQueue 
(  
)  !
;! "
QueueMessage 
[ 
] 
GetMessages "
(" #
QueueClient# .
queueClient/ :
): ;
;; <
} 
public 

class 
ReportsQueue 
: 
IReportsQueue  -
{ 
private 
readonly 
ILogger  
<  !
ReportsQueue! -
>- .
_logger/ 6
;6 7
private 
readonly 
AzureQueueConfig )
_azureQueueConfig* ;
;; <
public   
ReportsQueue   
(   
ILogger   #
<  # $
ReportsQueue  $ 0
>  0 1
logger  2 8
,!! 
IConfiguration!! 
configuration!! *
,"" 
IOptions"" 
<"" 
AzureQueueConfig"" '
>""' (
azureQueueConfig"") 9
)""9 :
{## 	
_logger$$ 
=$$ 
logger$$ 
;$$ 
_azureQueueConfig%% 
=%% 
azureQueueConfig%%  0
.%%0 1
Value%%1 6
;%%6 7
}&& 	
public(( 
async(( 
Task(( 
DequeueMessages(( )
((() *
QueueMessage((* 6
[((6 7
]((7 8
messages((9 A
,((A B
QueueClient((C N
queueClient((O Z
)((Z [
{)) 	
if** 
(** 
queueClient** 
.** 
Exists** "
(**" #
)**# $
)**$ %
{++ 
foreach,, 
(,, 
QueueMessage,, %
message,,& -
in,,. 0
messages,,1 9
),,9 :
{-- 
try.. 
{.. 
_logger// 
.// 
LogInformation// *
(//* +
$"//+ - 
De-queued message: '//- A
{//A B
message//B I
.//I J
MessageText//J U
}//U V
'//V W
"//W X
)//X Y
;//Y Z
await22 
queueClient22 %
.22% &
DeleteMessageAsync22& 8
(228 9
message229 @
.22@ A
	MessageId22A J
,22J K
message22L S
.22S T

PopReceipt22T ^
)22^ _
;22_ `
}33 
catch44 
(44 
	Exception44 #
exc44$ '
)44' (
{55 
_logger66 
.66  
LogError66  (
(66( )
$"66) +(
Could not dequeue message: '66+ G
{66G H
exc66H K
.66K L
Message66L S
}66S T
'66T U
"66U V
)66V W
;66W X
}77 
}88 
}99 
}:: 	
public== 
QueueMessage== 
[== 
]== 
GetMessages== )
(==) *
QueueClient==* 5
queueClient==6 A
)==A B
{>> 	
try?? 
{@@ 
returnAA 
queueClientAA "
.AA" #
ReceiveMessagesAA# 2
(AA2 3
$numAA3 5
)AA5 6
;AA6 7
}BB 
catchCC 
(CC 
	ExceptionCC 
excCC  
)CC  !
{DD 
_loggerEE 
.EE 
LogErrorEE  
(EE  !
$"EE! #4
(Could not receive messages from Queue: 'EE# K
{EEK L
excEEL O
.EEO P
MessageEEP W
}EEW X
'EEX Y
"EEY Z
)EEZ [
;EE[ \
returnFF 
nullFF 
;FF 
}GG 
}HH 	
publicJJ 
QueueClientJJ 
CreateQueueJJ &
(JJ& '
)JJ' (
{KK 	
stringMM 
connectionStringMM #
=MM$ %
_azureQueueConfigMM& 7
.MM7 8
StorageConnectionMM8 I
;MMI J
stringOO 
	queueNameOO 
=OO 
_azureQueueConfigOO 0
.OO0 1
	QueueNameOO1 :
;OO: ;
tryQQ 
{RR 
QueueClientWW 
queueClientWW '
=WW( )
newWW* -
QueueClientWW. 9
(WW9 :
connectionStringWW: J
,WWJ K
	queueNameWWL U
)WWU V
;WWV W
queueClientZZ 
.ZZ 
CreateIfNotExistsZZ -
(ZZ- .
)ZZ. /
;ZZ/ 0
if\\ 
(\\ 
queueClient\\ 
.\\  
Exists\\  &
(\\& '
)\\' (
)\\( )
{]] 
_logger^^ 
.^^ 
LogInformation^^ *
(^^* +
$"^^+ -
Queue created: '^^- =
{^^= >
queueClient^^> I
.^^I J
Name^^J N
}^^N O
'^^O P
"^^P Q
)^^Q R
;^^R S
return__ 
queueClient__ &
;__& '
}`` 
elseaa 
{bb 
_loggercc 
.cc 
LogInformationcc *
(cc* +
$"cc+ -I
=Make sure the Azurite storage emulator running and try again.cc- j
"ccj k
)cck l
;ccl m
returndd 
nulldd 
;dd  
}ee 
}ff 
catchgg 
(gg 
	Exceptiongg 
exgg 
)gg  
{hh 
_loggerii 
.ii 
LogInformationii &
(ii& '
$"ii' )
Exception: ii) 4
{ii4 5
exii5 7
.ii7 8
Messageii8 ?
}ii? @
\n\nii@ D
"iiD E
)iiE F
;iiF G
_loggerjj 
.jj 
LogInformationjj &
(jj& '
$"jj' )I
=Make sure the Azurite storage emulator running and try again.jj) f
"jjf g
)jjg h
;jjh i
returnkk 
nullkk 
;kk 
}ll 
}mm 	
}pp 
publicss 

classss 
AwsQueueClientss 
{tt 
privateuu 
staticuu 
asyncuu 
Taskuu !
<uu! ""
ReceiveMessageResponseuu" 8
>uu8 9

GetMessageuu: D
(uuD E

IAmazonSQSvv 

	sqsClientvv 
,vv 
stringvv 
qUrlvv !
,vv! "
intvv# &
waitTimevv' /
=vv0 1
$numvv2 3
,vv3 4
intvv5 8
MaxMessagesvv9 D
=vvE F
$numvvG I
)vvI J
{ww 	
returnxx 
awaitxx 
	sqsClientxx "
.xx" #
ReceiveMessageAsyncxx# 6
(xx6 7
newxx7 :!
ReceiveMessageRequestxx; P
{yy 
QueueUrlzz 
=zz 
qUrlzz 
,zz  
MaxNumberOfMessages{{ #
={{$ %
MaxMessages{{& 1
,{{1 2
WaitTimeSeconds|| 
=||  !
waitTime||" *
}~~ 
)~~ 
;~~ 
} 	
}
ÄÄ 
}ÇÇ ËK
t/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/ReportService/ReportServiceHosted.cs
	namespace 	
ReportService
 
{ 
public 

class 
ReportServiceHosted $
:% &
BackgroundService' 8
{ 
private 
readonly 
ILogger  
<  !
ReportServiceHosted! 4
>4 5
_logger6 =
;= >
private 
readonly 
IReportFile $
_reportFile% 0
;0 1
private 
readonly 
IReportsQueue &
_reportsQueue' 4
;4 5
private 
readonly 
ISqlExecuter %
_sqlExecuter& 2
;2 3
private 
readonly 
BaseDbContext &
_context' /
;/ 0
public 
ReportServiceHosted "
(" #
ILogger# *
<* +
ReportServiceHosted+ >
>> ?
logger@ F
,F G
IReportFile 

reportFile "
," #
IReportsQueue   
reportsQueue   &
,  & '
ISqlExecuter!! 
sqlExecuter!! $
,!!$ %
BaseDbContext"" 
context"" !
)""! "
{## 	
_logger$$ 
=$$ 
logger$$ 
;$$ 
_reportFile%% 
=%% 

reportFile%% $
;%%$ %
_reportsQueue&& 
=&& 
reportsQueue&& (
;&&( )
_sqlExecuter'' 
='' 
sqlExecuter'' &
;''& '
_context(( 
=(( 
context(( 
;(( 
})) 	
	protected++ 
override++ 
async++  
Task++! %
ExecuteAsync++& 2
(++2 3
CancellationToken++3 D
stoppingToken++E R
)++R S
{,, 	
_context-- 
.-- 
ChangeTracker-- "
.--" #!
QueryTrackingBehavior--# 8
=--9 :!
QueryTrackingBehavior--; P
.--P Q

NoTracking--Q [
;--[ \
var// 
QueueReports// 
=// 
_reportsQueue// ,
.//, -
CreateQueue//- 8
(//8 9
)//9 :
;//: ;
while11 
(11 
!11 
stoppingToken11 !
.11! "#
IsCancellationRequested11" 9
)119 :
{22 
try44 
{55 
var77 
messages77  
=77! "
_reportsQueue77# 0
.770 1
GetMessages771 <
(77< =
QueueReports77= I
)77I J
;77J K
var99 
ConfiguredReports99 )
=99* +
_context99, 4
.994 5 
ReportConfigurations995 I
.99I J
ToList99J P
(99P Q
)99Q R
;99R S
while;; 
(;; 
messages;; #
.;;# $
Count;;$ )
(;;) *
);;* +
>;;, -
$num;;. /
&&;;0 2
messages;;3 ;
[;;; <
$num;;< =
];;= >
is;;? A
not;;B E
null;;F J
);;J K
{<< 
_logger>> 
.>>  
LogInformation>>  .
(>>. /
$">>/ 1
'>>1 2
{>>2 3
DateTime>>3 ;
.>>; <
Now>>< ?
}>>? @#
': Messages received: '>>@ W
{>>W X
messages>>X `
.>>` a
Count>>a f
(>>f g
)>>g h
}>>h i
'>>i j
">>j k
)>>k l
;>>l m
Parallel@@  
.@@  !
ForEach@@! (
(@@( )
messages@@) 1
,@@1 2
new@@3 6
ParallelOptions@@7 F
{@@G H"
MaxDegreeOfParallelism@@I _
=@@` a
$num@@b c
}@@d e
,@@e f
async@@g l
message@@m t
=>@@u w
{AA 
byteBB  
[BB  !
]BB! "
dataBB# '
=BB( )
ConvertBB* 1
.BB1 2
FromBase64StringBB2 B
(BBB C
messageBBC J
.BBJ K
MessageTextBBK V
)BBV W
;BBW X
stringDD "#
ReportNameDecodedStringDD# :
=DD; <
EncodingDD= E
.DDE F
UTF8DDF J
.DDJ K
	GetStringDDK T
(DDT U
dataDDU Y
)DDY Z
;DDZ [
ifGG 
(GG 
ConfiguredReportsGG 0
.GG0 1
WhereGG1 6
(GG6 7
xGG7 8
=>GG9 ;
xGG< =
.GG= >
Report_NameGG> I
==GGJ L#
ReportNameDecodedStringGGM d
)GGd e
.GGe f
CountGGf k
(GGk l
)GGl m
>GGn o
$numGGp q
)GGq r
{HH 
	DataTableJJ  )

ReportDataJJ* 4
=JJ5 6
_sqlExecuterJJ7 C
.JJC D
GetSqlFeedbackJJD R
(JJR S
ConfiguredReportsJJS d
.JJd e
WhereJJe j
(JJj k
xJJk l
=>JJm o
xJJp q
.JJq r
Report_NameJJr }
==	JJ~ Ä%
ReportNameDecodedString
JJÅ ò
)
JJò ô
.
JJô ö
First
JJö ü
(
JJü †
)
JJ† °
.
JJ° ¢

Report_Sql
JJ¢ ¨
,
JJ¨ ≠
DateTime
JJÆ ∂
.
JJ∂ ∑
Now
JJ∑ ∫
)
JJ∫ ª
;
JJª º
ifMM  "
(MM# $

ReportDataMM$ .
isMM/ 1
notMM2 5
nullMM6 :
)MM: ;
{NN  !
awaitOO$ )
_reportFileOO* 5
.OO5 6
ReportAsFileAsyncOO6 G
(OOG H

ReportDataOOH R
,OOR S
ConfiguredReportsPP( 9
.PP9 :
WherePP: ?
(PP? @
xPP@ A
=>PPB D
xPPE F
.PPF G
Report_NamePPG R
==PPS U#
ReportNameDecodedStringPPV m
)PPm n
.PPn o
FirstPPo t
(PPt u
)PPu v
.PPv w
Report_Name	PPw Ç
,
PPÇ É
$strQQ( *
,QQ* +
ConfiguredReportsRR( 9
.RR9 :
WhereRR: ?
(RR? @
xRR@ A
=>RRB D
xRRE F
.RRF G
Report_NameRRG R
==RRS U#
ReportNameDecodedStringRRV m
)RRm n
.RRn o
FirstRRo t
(RRt u
)RRu v
.RRv w
Report_Separator	RRw á
,
RRá à
ConfiguredReportsSS( 9
.SS9 :
WhereSS: ?
(SS? @
xSS@ A
=>SSB D
xSSE F
.SSF G
Report_NameSSG R
==SSS U#
ReportNameDecodedStringSSV m
)SSm n
.SSn o
FirstSSo t
(SSt u
)SSu v
.SSv w

Header_Row	SSw Å
)
SSÅ Ç
;
SSÇ É
_loggerTT$ +
.TT+ ,
LogInformationTT, :
(TT: ;
$"TT; =
'TT= >
{TT> ?
DateTimeTT? G
.TTG H
NowTTH K
}TTK L
': Report 'TTL W
{TTW X
ConfiguredReportsTTX i
.TTi j
WhereTTj o
(TTo p
xTTp q
=>TTr t
xTTu v
.TTv w
Report_Name	TTw Ç
==
TTÉ Ö%
ReportNameDecodedString
TTÜ ù
)
TTù û
.
TTû ü
First
TTü §
(
TT§ •
)
TT• ¶
.
TT¶ ß
Report_Name
TTß ≤
}
TT≤ ≥

' created.
TT≥ Ω
"
TTΩ æ
)
TTæ ø
;
TTø ¿
}UU  !
elseVV  $
{WW  !
_loggerXX$ +
.XX+ ,
LogErrorXX, 4
(XX4 5
$"XX5 7
No data found for 'XX7 J
{XXJ K#
ReportNameDecodedStringXXK b
}XXb c
'XXc d
"XXd e
)XXe f
;XXf g
}YY  !
}ZZ 
else[[  
{\\ 
_logger]]  '
.]]' (
LogError]]( 0
(]]0 1
$"]]1 3
']]3 4
{]]4 5#
ReportNameDecodedString]]5 L
}]]L M)
' is not a configured report!]]M j
"]]j k
)]]k l
;]]l m
}^^ 
}aa 
)aa 
;aa 
awaitcc 
_reportsQueuecc +
.cc+ ,
DequeueMessagescc, ;
(cc; <
messagescc< D
,ccD E
QueueReportsccF R
)ccR S
;ccS T
Arrayee 
.ee 
Clearee #
(ee# $
messagesee$ ,
,ee, -
$numee- .
,ee. /
messagesee/ 7
.ee7 8
Lengthee8 >
)ee> ?
;ee? @
}gg 
_loggerii 
.ii 
LogInformationii *
(ii* +
$strii+ X
)iiX Y
;iiY Z
awaitkk 
Taskkk 
.kk 
Delaykk $
(kk$ %
$numkk% *
,kk* +
stoppingTokenkk, 9
)kk9 :
;kk: ;
}mm 
catchnn 
(nn 
	Exceptionnn 
excnn  #
)nn# $
{oo 
_loggerpp 
.pp 
LogCriticalpp '
(pp' (
stringpp( .
.pp. /
Concatpp/ 5
(pp5 6

MethodBasepp6 @
.pp@ A
GetCurrentMethodppA Q
(ppQ R
)ppR S
.ppS T
DeclaringTypeppT a
.ppa b
Nameppb f
,ppf g
$str	ppg í
,
ppí ì
exc
ppî ó
.
ppó ò
Message
ppò ü
)
pp† °
)
pp° ¢
;
pp¢ £
}qq 
}uu 
}vv 	
}ww 
}xx 