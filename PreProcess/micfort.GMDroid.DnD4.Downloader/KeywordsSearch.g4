grammar KeywordsSearch;

/*
 * Parser Rules
 */

search : header data;
header : '<?xml' text '?>';
data : '<Data>' results totals '</Data>';

results: '<Results>' (monster)* '</Results>';
monster: '<Monster>' id name level groupRole combatRole sourceBook '</Monster>';

id: idOE NUMBER idCE;
idOE: OT 'ID' CT;
idCE: OT CE 'ID' CT;
name: nameOE text nameCE;
nameOE: OT 'Name' CT;
nameCE: OT CE 'Name' CT;
level: levelOE NUMBER levelCE;
levelOE: OT 'Level' CT;
levelCE: OT CE 'Level' CT;
groupRole: groupRoleOE GROUPROLE groupRoleCE;
groupRoleOE: OT 'GroupRole' CT;
groupRoleCE: OT CE 'GroupRole' CT;
combatRole: combatRoleOE (COMBATROLE (',' LEADER)? | LEADER ',' COMBATROLE ) combatRoleCE;
combatRoleOE: OT 'CombatRole' CT;
combatRoleCE: OT CE 'CombatRole' CT;
sourceBook: sourceBookOE text sourceBookCE;
sourceBookOE: OT 'SourceBook' CT;
sourceBookCE: OT CE 'SourceBook' CT;

totals: '<Totals>' tab '</Totals>';
tab : '<Tab>' (table total | total table) '</Tab>';
table : '<Table>' STRING '</Table>';
total : '<Total>' NUMBER '</Total>';

text : (~(WS|OT|CT))*;

/*
 * Lexer Rules
 */
OT : '<';
CT : '>';
CE : '/';
GROUPROLE: 'Solo'|'Minion'|'Elite'|'Standard'|'Conjured';
COMBATROLE: 'Brute'|'Controller'|'Soldier'|'Skirmisher'|'Artillery'|'Lurker'|'No Role'|'No role';
LEADER: 'Leader';
STRING : [a-zA-Z]+;
NUMBER : [0-9]+;
SPECIALSYMBOLS: (~([ a-zA-Z0-9<>]));
WS : [ \t\r\n]+ -> skip;
