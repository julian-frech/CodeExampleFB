⁄
ä/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/ReportWriter/ConfigurationOptions/CloudFileWriterConfig.cs
	namespace 	
ReportWriter
 
. 
ConfigurationOption *
{ 
public 

class !
CloudFileWriterConfig &
{ 
public 
const 
string (
CloudFileWriterConfigSection 8
=9 :
$str; R
;R S
public		 
string		 

BaseFolder		  
{		! "
get		# &
;		& '
set		( +
;		+ ,
}		- .
public 
string 
StorageConnection (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
string 
ContainerName #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public !
CloudFileWriterConfig $
($ %
)% &
{ 	
} 	
} 
} Ä
Ö/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/ReportWriter/ConfigurationOptions/FileWriterConfig.cs
	namespace 	
ReportWriter
 
. 
ConfigurationOption *
{ 
public 

class 
FileWriterConfig !
{ 
public 
const 
string #
FileWriterConfigSection 3
=4 5
$str6 H
;H I
public 
string 

BaseFolder  
{! "
get# &
;& '
set( +
;+ ,
}- .
public

 
FileWriterConfig

 
(

  
)

  !
{ 	
} 	
} 
} √
l/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/ReportWriter/ReportWriter.cs
	namespace 	
ReportWriter
 
{ 
public 

	interface 
IReportFile  
{ 
Task 
ReportAsFileAsync 
( 
	DataTable (
reportAsdataTable) :
,: ;
string< B
fileNameC K
,K L
stringM S#
targetLocationExtensionT k
,k l
stringm s
	delimitert }
,} ~
string	 Ö
	headerRow
Ü è
)
è ê
;
ê ë
} 
public 

class 

ReportFile 
: 
IReportFile )
{ 
private 
readonly 
IFileWriter $
_fileWriter% 0
;0 1
public   

ReportFile   
(   
IFileWriter   %

fileWriter  & 0
)  0 1
{!! 	
_fileWriter"" 
="" 

fileWriter"" $
;""$ %
}## 	
public-- 
async-- 
Task-- 
ReportAsFileAsync-- +
(--+ ,
	DataTable--, 5
reportAsdataTable--6 G
,--G H
string--I O
fileName--P X
,--X Y
string--Z `#
targetLocationExtension--a x
,--x y
string	--z Ä
	delimiter
--Å ä
,
--ä ã
string
--å í
	headerRow
--ì ú
)
--ú ù
{.. 	
await00 
_fileWriter00 
.00 
StringToFile00 *
(00* +#
targetLocationExtension00+ B
+00C D
fileName00E M
,00M N
reportAsdataTable11$ 5
.115 6
ToCsv116 ;
(11; <
	delimiter11< E
,11E F
	headerRow11G P
)11P Q
)22$ %
;22% &
}33 	
}44 
}55 ¬
Ü/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/ReportWriter/Services/Extensions/DataTableExtension.cs
	namespace 	
ReportWriter
 
. 
Service 
. 

Extensions )
{		 
public

 

static

 
class

 
DataTableExtension

 *
{ 
public 
static 
string 
ToCsvParallel *
(* +
this+ /
	DataTable0 9
	datatable: C
,C D
stringE K
	delimiterL U
,U V
intW Z

maxThreads[ e
,e f
stringg m
	headerRown w
)w x
{ 	
var 
result 
= 
new 
StringBuilder *
(* +
)+ ,
;, -
result 
. 
Append 
( 
	headerRow #
)# $
;$ %
Parallel 
. 
ForEach 
( 
	datatable &
.& '
AsEnumerable' 3
(3 4
)4 5
,5 6
new7 :
ParallelOptions; J
{K L"
MaxDegreeOfParallelismM c
=d e

maxThreadsf p
}q r
,r s
drowt x
=>y {
{ 
IEnumerable 
< 
string "
>" #
columns$ +
=, -
drow. 2
.2 3
	ItemArray3 <
.< =
Select= C
(C D
columnD J
=>K M
columnN T
.T U
ToStringU ]
(] ^
)^ _
)_ `
;` a
result 
. 
Append 
( 
$char "
+" #
string# )
.) *
Join* .
(. /
	delimiter/ 8
,8 9
columns: A
)A B
)B C
;C D
} 
) 
; 
return 
result 
. 
ToString "
(" #
)# $
;$ %
} 	
public&& 
static&& 
string&& 
ToCsv&& "
(&&" #
this&&# '
	DataTable&&( 1
	datatable&&2 ;
,&&; <
string&&= C
	delimiter&&D M
,&&M N
string&&O U
	headerRow&&V _
)&&_ `
{'' 	
var(( 
result(( 
=(( 
new(( 
StringBuilder(( *
(((* +
)((+ ,
;((, -
result)) 
.)) 

AppendLine)) 
()) 
	headerRow)) '
)))' (
;))( )
foreach++ 
(++ 
DataRow++ 
row++  
in++! #
	datatable++$ -
.++- .
Rows++. 2
)++2 3
{,, 
IEnumerable-- 
<-- 
string-- "
>--" #
columns--$ +
=--, -
row--. 1
.--1 2
	ItemArray--2 ;
.--; <
Select--< B
(--B C
column--C I
=>--J L
column--M S
.--S T
ToString--T \
(--\ ]
)--] ^
)--^ _
;--_ `
result.. 
... 

AppendLine.. !
(..! "
string.." (
...( )
Join..) -
(..- .
	delimiter... 7
,..7 8
columns..9 @
)..@ A
)..A B
;..B C
}// 
return11 
result11 
.11 
ToString11 "
(11" #
)11# $
;11$ %
}33 	
}55 
}66 Å5
t/Users/julianfrech/Documents/GitHub/CodeExampleFB/ReportGenerator/ReportService/ReportWriter/Services/IFileWriter.cs
	namespace 	
ReportWriter
 
. 
Service 
{ 
public 

	interface 
IFileWriter  
{ 
Task 
StringToFile 
( 
string  

targetFile! +
,+ ,
string- 3
inputString4 ?
)? @
;@ A
} 
public 

class 

FileWriter 
: 
IFileWriter (
{ 
private 
readonly 
ILogger  
<  !

FileWriter! +
>+ ,
_logger- 4
;4 5
private 
readonly 
FileWriterConfig )
_fileWriterConfig* ;
;; <
public 

FileWriter 
( 
ILogger !
<! "

FileWriter" ,
>, -
logger. 4
,4 5
IOptions6 >
<> ?
FileWriterConfig? O
>O P
fileWriterConfigQ a
)a b
{ 	
_logger 
= 
logger 
; 
_fileWriterConfig 
= 
fileWriterConfig  0
.0 1
Value1 6
;6 7
} 	
public 
async 
Task 
StringToFile &
(& '
string' -

targetFile. 8
,8 9
string: @
inputStringA L
)L M
{ 	
try   
{!! 
var"" 
target"" 
="" 
_fileWriterConfig"" .
."". /

BaseFolder""/ 9
+"": ;

targetFile""< F
;""F G
_logger$$ 
.$$ 
LogInformation$$ &
($$& '
$str$$' 5
+$$6 7
target$$8 >
)$$> ?
;$$? @
File&& 
.&& 
WriteAllTextAsync&& &
(&&& '
target&&' -
,&&- .
inputString&&/ :
)&&: ;
;&&; <
}(( 
catch)) 
()) 
	Exception)) 
exc)) 
)))  
{** 
_logger++ 
.++ 
LogInformation++ &
(++& '
exc++' *
.++* +
Message+++ 2
)++2 3
;++3 4
},, 
}.. 	
}// 
public11 

class11 
CloudFileWriter11  
:11! "
IFileWriter11# .
{22 
private33 
readonly33 
ILogger33  
<33  !
CloudFileWriter33! 0
>330 1
_logger332 9
;339 :
private55 
readonly55 !
CloudFileWriterConfig55 ."
_cloudFileWriterConfig55/ E
;55E F
public77 
CloudFileWriter77 
(77 
ILogger77 &
<77& '
CloudFileWriter77' 6
>776 7
logger778 >
,88 
IOptions88 
<88 !
CloudFileWriterConfig88 ,
>88, -!
cloudFileWriterConfig88. C
)88C D
{99 	
_logger:: 
=:: 
logger:: 
;:: "
_cloudFileWriterConfig;; "
=;;# $!
cloudFileWriterConfig;;% :
.;;: ;
Value;;; @
;;;@ A
}<< 	
public>> 
async>> 
Task>> 
StringToFile>> &
(>>& '
string>>' -

targetFile>>. 8
,>>8 9
string>>: @
inputString>>A L
)>>L M
{?? 	
stringAA 
stoargeConnectionAA $
=AA% &"
_cloudFileWriterConfigAA' =
.AA= >
StorageConnectionAA> O
;AAO P
stringCC 
containerNameCC  
=CC! ""
_cloudFileWriterConfigCC# 9
.CC9 :
ContainerNameCC: G
;CCG H
stringEE 
targetEE 
=EE "
_cloudFileWriterConfigEE 2
.EE2 3

BaseFolderEE3 =
+EE> ?

targetFileEE@ J
;EEJ K
tryGG 
{HH 
CloudStorageAccountII #
cloudStorageAccountII$ 7
=II8 9
CloudStorageAccountII: M
.IIM N
ParseIIN S
(IIS T
stoargeConnectionIIT e
)IIe f
;IIf g
CloudBlobClientKK 
cloudBlobClientKK  /
=KK0 1
cloudStorageAccountKK2 E
.KKE F!
CreateCloudBlobClientKKF [
(KK[ \
)KK\ ]
;KK] ^
CloudBlobContainerMM "
cloudBlobContainerMM# 5
=MM6 7
cloudBlobClientMM8 G
.MMG H!
GetContainerReferenceMMH ]
(MM] ^
containerNameMM^ k
)MMk l
;MMl m
ifOO 
(OO 
awaitOO 
cloudBlobContainerOO ,
.OO, -"
CreateIfNotExistsAsyncOO- C
(OOC D
)OOD E
)OOE F
{PP 
awaitQQ 
cloudBlobContainerQQ ,
.QQ, -
SetPermissionsAsyncQQ- @
(QQ@ A
newRR $
BlobContainerPermissionsRR 4
{RR5 6
PublicAccessRR7 C
=RRD E)
BlobContainerPublicAccessTypeRRF c
.RRc d
BlobRRd h
}RRi j
)RRj k
;RRk l
}SS 
CloudBlockBlobUU 
cloudBlockBlobUU -
=UU. /
cloudBlobContainerUU0 B
.UUB C!
GetBlockBlobReferenceUUC X
(UUX Y
targetUUY _
)UU_ `
;UU` a
varWW 

fileStreamWW 
=WW  
newWW! $
MemoryStreamWW% 1
(WW1 2
EncodingWW2 :
.WW: ;
UTF8WW; ?
.WW? @
GetBytesWW@ H
(WWH I
inputStringWWI T
??WWU W
$strWWX Z
)WWZ [
)WW[ \
;WW\ ]
cloudBlockBlobYY 
.YY !
UploadFromStreamAsyncYY 4
(YY4 5

fileStreamYY5 ?
)YY? @
;YY@ A
_logger[[ 
.[[ 
LogInformation[[ &
([[& '
$"[[' )
'[[) *
{[[* +
target[[+ 1
}[[1 2
' uploaded to '[[2 A
{[[A B
containerName[[B O
}[[O P
'[[P Q
"[[Q R
)[[R S
;[[S T
}]] 
catch^^ 
(^^ 
	Exception^^ 
exc^^  
)^^  !
{__ 
_logger`` 
.`` 
LogCritical`` #
(``# $
exc``$ '
.``' (
Message``( /
)``/ 0
;``0 1
}aa 
}cc 	
}dd 
}ff 