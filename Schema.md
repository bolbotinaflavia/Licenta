flowchart BT
AnimSoundManager(AnimSoundManager)
Artefact(Artefact)
ArtefactBase(ArtefactBase)
Artefact_select(Artefact_select)
Attack(Attack)
BattleAnimationManager(BattleAnimationManager)
BattleState(BattleState)
BattleSystem(BattleSystem)
BattleUnit(BattleUnit)
Box(Box)
ControlMenu(ControlMenu)
Controls(Controls)
Defense(Defense)
Door(Door)
EnemieBase(EnemieBase)
Enemy(Enemy)
ExitFight(ExitFight)
ExitGame(ExitGame)
EyeTrack(EyeTrack)
FightScene(FightScene)
Food(Food)
FoodBase(FoodBase)
GameController(GameController)
GameState(GameState)
HpBar(HpBar)
HpBarAnimation(HpBarAnimation)
HpSlider(HpSlider)
IControl(IControl)
IPlayerActions(IPlayerActions)
InventoryManager(InventoryManager)
Keyboard(Keyboard)
LearnableMoves(LearnableMoves)
MagicTree(MagicTree)
MagicianDialogue(MagicianDialogue)
MenuCountdown(MenuCountdown)
MenuManager(MenuManager)
Mouse(Mouse)
Moves(Moves)
NextMenu(NextMenu)
OpenPlayerMenu(OpenPlayerMenu)
PlayerActions(PlayerActions)
PlayerManager(PlayerManager)
PlayerMovement(PlayerMovement)
PlayerUnit(PlayerUnit)
PreviuosMenu(PreviuosMenu)
RaycastDebugger(RaycastDebugger)
Restart_game(Restart_game)
Run(Run)
SaveButton(SaveButton)
SelectItem(SelectItem)
Spell(Spell)
Spell(Spell)
SpellBase(SpellBase)
SpellLevel(SpellLevel)
SpellSelect(SpellSelect)
SpellWeakness(SpellWeakness)
StartGame(StartGame)
Type(Type)
Volume(Volume)
VolumeMenu(VolumeMenu)
Weapon(Weapon)
WeaponB(WeaponB)
WeaponWeakness(WeaponWeakness)
WeaponsMenu(WeaponsMenu)
control_pregame(control_pregame)
load_anim_battle(load_anim_battle)

Artefact  -->  ArtefactBase 
Artefact  -..->  ArtefactBase 
Artefact_select  -..->  ArtefactBase 
Artefact_select  -..-|>  MenuCountdown 
Attack  -..-|>  MenuCountdown 
BattleAnimationManager  -..->  BattleUnit 
BattleAnimationManager  -..->  HpBar 
BattleAnimationManager  -..->  HpBarAnimation 
BattleAnimationManager  -..->  HpSlider 
BattleAnimationManager  -..->  PlayerUnit 
BattleSystem  -..->  BattleAnimationManager 
BattleSystem  -->  BattleAnimationManager 
BattleSystem  -..->  BattleState 
BattleSystem  -->  BattleState 
BattleSystem  -..->  BattleSystem 
BattleSystem  -..->  BattleUnit 
BattleSystem  -->  BattleUnit 
BattleSystem  -..->  HpBar 
BattleSystem  -..->  HpSlider 
BattleSystem  -..->  PlayerUnit 
BattleUnit  -..->  EnemieBase 
BattleUnit  -->  EnemieBase 
BattleUnit  -..->  HpBarAnimation 
BattleUnit  -->  Moves 
Box  -..->  Box 
ControlMenu  -..-|>  MenuCountdown 
ControlMenu  -..->  PlayerMovement 
Controls  -->  PlayerActions 
Defense  -..-|>  MenuCountdown 
Door  -..->  Door 
EnemieBase  -..->  SpellWeakness 
EnemieBase  -->  SpellWeakness 
EnemieBase  -..->  Type 
EnemieBase  -->  WeaponWeakness 
EnemieBase  -..->  WeaponWeakness 
Enemy  -..->  EnemieBase 
Enemy  -->  EnemieBase 
ExitFight  -..-|>  MenuCountdown 
ExitGame  -..-|>  MenuCountdown 
EyeTrack  -..->  EyeTrack 
EyeTrack  -..-|>  IControl 
FightScene  -..-|>  MenuCountdown 
Food  -->  FoodBase 
Food  -..->  FoodBase 
GameController  -..->  BattleSystem 
GameController  -..->  GameController 
GameController  -..->  GameState 
GameController  -..->  PlayerManager 
HpBar  -..->  BattleUnit 
HpBar  -..->  HpBar 
HpSlider  -..->  HpSlider 
InventoryManager  -->  ArtefactBase 
InventoryManager  -..->  InventoryManager 
InventoryManager  -->  Spell 
InventoryManager  -..->  WeaponB 
InventoryManager  -->  WeaponB 
Keyboard  -..-|>  IControl 
LearnableMoves  -..->  Moves 
LearnableMoves  -->  Moves 
MagicTree  -..->  MagicTree 
MagicTree  -..->  Spell 
MagicianDialogue  -..->  MagicianDialogue 
MenuCountdown  -..->  MenuCountdown 
MenuManager  -..->  MenuManager 
Mouse  -..-|>  IControl 
Moves  -..->  Type 
NextMenu  -..-|>  MenuCountdown 
OpenPlayerMenu  -..-|>  MenuCountdown 
PlayerActions  -..->  Controls 
PlayerActions  --*  Controls 
PlayerManager  -->  HpBarAnimation 
PlayerManager  -..->  HpBarAnimation 
PlayerManager  -..->  InventoryManager 
PlayerManager  -->  InventoryManager 
PlayerManager  -..->  PlayerManager 
PlayerManager  -..->  PlayerMovement 
PlayerMovement  -..->  IControl 
PlayerMovement  -..->  PlayerManager 
PlayerMovement  -->  PlayerMovement 
PlayerMovement  -..->  PlayerMovement 
PlayerUnit  -->  HpBarAnimation 
PlayerUnit  -..->  HpBarAnimation 
PlayerUnit  -..->  PlayerManager 
PlayerUnit  -->  WeaponB 
PreviuosMenu  -..-|>  MenuCountdown 
Restart_game  -..-|>  MenuCountdown 
Run  -..-|>  MenuCountdown 
SaveButton  -..-|>  MenuCountdown 
SelectItem  -..->  FoodBase 
SelectItem  -..-|>  MenuCountdown 
Spell  -..->  SpellBase 
Spell  -->  SpellBase 
Spell  -..->  SpellLevel 
Spell  -..->  SpellLevel 
SpellSelect  -..-|>  MenuCountdown 
SpellSelect  -..->  Spell 
StartGame  -..-|>  MenuCountdown 
Volume  -..->  Volume 
VolumeMenu  -..-|>  MenuCountdown 
VolumeMenu  -..->  Volume 
VolumeMenu  -..->  VolumeMenu 
Weapon  -->  WeaponB 
Weapon  -..->  WeaponB 
WeaponsMenu  -..-|>  MenuCountdown 
WeaponsMenu  -..->  WeaponB 
control_pregame  -..->  PlayerMovement 
