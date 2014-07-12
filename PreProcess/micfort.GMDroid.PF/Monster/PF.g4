grammar PF;

pf : header (monster)+;

header : line;

line : cell (SEP cell)* NEWLINE;

cell : QUOTE quotedText QUOTE 
     | text
     |
     ;

text : (~(SEP | NEWLINE | QUOTE))*;
quotedText : (~(NEWLINE | QUOTE))*;

posNegNumber : ('+'|'-')? NUMBER;

STRING : ' '? ([a-zA-Z]* [a-zA-Z ]+ [a-zA-Z]*) ' '?;
NUMBER : [0-9]+;
SPECIALSYMBOLS: (~([ a-zA-Z0-9]|'\r'|'\n'|','|'"'));
NEWLINE: '\r'? '\n' ;
SEP    : ',';
QUOTE  : '"';
DOUBLEQUOTE : '""';
WS : [ \t]+ -> skip;


monster : nameCell ',' crCell ',' xpCell ',' raceCell ',' mclassCell ',' monstersourceCell ',' alignmentCell ',' sizeCell ',' typeCell ',' subtypeCell ',' initCell ',' sensesCell ',' auraCell ',' acCell ',' ac_modsCell ',' hpCell ',' hdCell ',' hp_modsCell ',' savesCell ',' fortCell ',' refCell ',' willCell ',' save_modsCell ',' defensiveabilitiesCell ',' drCell ',' immuneCell ',' resistCell ',' srCell ',' weaknessesCell ',' speedCell ',' speed_modCell ',' meleeCell ',' rangedCell ',' spaceCell ',' reachCell ',' specialattacksCell ',' spelllikeabilitiesCell ',' spellsknownCell ',' spellspreparedCell ',' spelldomainsCell ',' abilityscoresCell ',' abilityscore_modsCell ',' baseatkCell ',' cmbCell ',' cmdCell ',' featsCell ',' skillsCell ',' racialmodsCell ',' languagesCell ',' sqCell ',' environmentCell ',' organizationCell ',' treasureCell ',' description_visualCell ',' groupCell ',' sourceCell ',' istemplateCell ',' specialabilitiesCell ',' descriptionCell ',' fulltextCell ',' genderCell ',' bloodlineCell ',' prohibitedschoolsCell ',' beforecombatCell ',' duringcombatCell ',' moraleCell ',' gearCell ',' othergearCell ',' vulnerabilityCell ',' noteCell ',' characterflagCell ',' companionflagCell ',' flyCell ',' climbCell ',' burrowCell ',' swimCell ',' landCell ',' templatesappliedCell ',' offensenoteCell ',' basestatisticsCell ',' extractspreparedCell ',' agecategoryCell ',' mysteryCell ',' classarchetypesCell ',' patronCell ',' companionfamiliarlinkCell ',' focusedschoolCell ',' traitsCell ',' alternatenameformCell ',' linktextCell ',' idCell ',' uniquemonsterCell ',' thassilonianspecializationCell ',' variantCell ',' mrCell ',' mythicCell ',' mtCell
        NEWLINE;

nameCell : cell;
crCell : cr;
cr: NUMBER | (NUMBER '/' NUMBER);
xpCell : NUMBER;
raceCell : cell;
mclassCell : (mclass ('/' mclass)*)?;
mclass : mclassName ' '* NUMBER | 'NULL';
mclassName : STRING ('(' STRING ')')?;
monstersourceCell : cell;
alignmentCell : 'CE' | 'CN' | 'CG' | 'NE' | 'N' | 'NG' | 'LE' | 'LN' | 'LG';
sizeCell : 'Tiny' | 'Small' | 'Medium' | 'Large' | 'Huge' | 'Gargantuan' | 'Diminutive' | 'Colossal';
typeCell : cell;
subtypeCell : cell;
initCell : QUOTE init QUOTE | init;
init : posNegNumber 'M'? ('/' posNegNumber 'M'? )? ' '? ('(' posNegNumber STRING ')' | ',' STRING)?;
sensesCell : QUOTE senses QUOTE | senses;
senses : (sense (SEP sense)* (';'))? skill;
sense: name (distance)?;
auraCell : cell;
acCell : QUOTE armorClass SEP armorClassTouch SEP armorClassFlatFooted QUOTE;
armorClass : 'AC '? NUMBER;
armorClassTouch : ' touch ' NUMBER;
armorClassFlatFooted: ' flat-footed ' NUMBER;
ac_modsCell : ac_mods | QUOTE ac_mods QUOTE | ;
ac_mods : '(' ac_mod (SEP ac_mod)* ')' ('(' ac_modSpecial (SEP ac_modSpecial)* ')')?;
ac_mod : ' '? modifier name;
ac_modSpecial : posNegNumber (STRING|'.')+;
hpCell : NUMBER ('each'|' each')?;
//hdCell : cell;
hdCell : hd | QUOTE hd QUOTE;
hd : '(' (NUMBER (' HD'|'HD') (';'|SEP) ' '?)? dices ')';
hp_modsCell : cell;
savesCell : QUOTE saves QUOTE;
saves: savesFort savesRef savesWill (';' quotedText)?;
savesFort : 'Fort ' modifier;
savesRef : ', Ref ' modifier;
savesWill : ', Will ' modifier;
fortCell : cell;
refCell : cell;
willCell : cell;
save_modsCell : cell;
defensiveabilitiesCell : cell;
drCell : QUOTE drList QUOTE | dr |;
drList: dr ((', '|',') dr)*;
dr: NUMBER '/' (text | '-');
immuneCell : QUOTE immuneList QUOTE | immune |;
immuneList: immune ((', '|',') immune)*;
immune: text;
resistCell : QUOTE restistList QUOTE | resist |;
restistList: resist ((', '|',') resist)*;
resist : STRING NUMBER;
srCell : NUMBER |;
weaknessesCell : cell;
speedCell : cell;
speed_modCell : cell;
meleeCell : cell;
rangedCell : cell;
spaceCell : cell;
reachCell : cell;
specialattacksCell : cell;
spelllikeabilitiesCell : cell;
spellsknownCell : cell;
spellspreparedCell : cell;
spelldomainsCell : cell;
abilityscoresCell : cell;
abilityscore_modsCell : cell;
baseatkCell : cell;
cmbCell : cell;
cmdCell : cell;
featsCell : cell;
skillsCell : skills | QUOTE skills QUOTE;
skills : (skill (SEP skill)*)?;
racialmodsCell : cell;
languagesCell : cell;
sqCell : cell;
environmentCell : cell;
organizationCell : cell;
treasureCell : cell;
description_visualCell : cell;
groupCell : cell;
sourceCell : cell;
istemplateCell : cell;
specialabilitiesCell : cell;
descriptionCell : cell;
fulltextCell : cell;
genderCell : cell;
bloodlineCell : cell;
prohibitedschoolsCell : cell;
beforecombatCell : cell;
duringcombatCell : cell;
moraleCell : cell;
gearCell : cell;
othergearCell : cell;
vulnerabilityCell : cell;
noteCell : cell;
characterflagCell : cell;
companionflagCell : cell;
flyCell : cell;
climbCell : cell;
burrowCell : cell;
swimCell : cell;
landCell : cell;
templatesappliedCell : cell;
offensenoteCell : cell;
basestatisticsCell : cell;
extractspreparedCell : cell;
agecategoryCell : cell;
mysteryCell : cell;
classarchetypesCell : cell;
patronCell : cell;
companionfamiliarlinkCell : cell;
focusedschoolCell : cell;
traitsCell : cell;
alternatenameformCell : cell;
linktextCell : cell;
idCell : cell;
uniquemonsterCell : cell;
thassilonianspecializationCell : cell;
variantCell : cell;
mrCell : cell;
mythicCell : cell;
mtCell : cell;


skill : skillName posNegNumber (' ' '(' skillSpecial (SEP skillSpecial)* ')')? ;
skillName : name ('(' STRING (',' ' '? STRING )* ')')? ' '?;
skillSpecial : posNegNumber (STRING|'.')+;
distance : NUMBER  ' ft' '.';
name : STRING ('-' name)*;

dices : dice ('+' dice)* modifier?;
dice : diceCount 'd' diceSides;
diceSides : NUMBER;
diceCount : NUMBER;
modifier : ('+'|'-') NUMBER;