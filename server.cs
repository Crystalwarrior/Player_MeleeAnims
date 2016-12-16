//NOTE TO SELF: TSShapeConstructor has to be done BEFORE player datablock.
datablock TSShapeConstructor(mMeleeDts) {
	baseShape = "base/data/shapes/player/mmelee.dts";
	sequence0 = "base/data/shapes/player/melee.dsq";
};

datablock PlayerData(PlayerMeleeAnims : PlayerStandardArmor)
{
	shapeFile = "base/data/shapes/player/mmelee.dts";
	canJet = 0;
	uiName = "Melee Anims Player";
};

function player::fixAppearance(%pl,%cl)
{
	%pl.hideNode("ALL");
	%pl.unHideNode((%cl.chest ? "femChest" : "chest"));
	
	%pl.unHideNode((%cl.rhand ? "rhook" : "rhand"));
	%pl.unHideNode((%cl.lhand ? "lhook" : "lhand"));
	%pl.unHideNode((%cl.rarm ? "rarmSlim" : "rarm"));
	%pl.unHideNode((%cl.larm ? "larmSlim" : "larm"));
	%pl.unHideNode("headskin");
	if($pack[%cl.pack] !$= "none")
	{
		%pl.unHideNode($pack[%cl.pack]);
		%pl.setNodeColor($pack[%cl.pack],%cl.packColor);
	}
	if($secondPack[%cl.secondPack] !$= "none")
	{
		%pl.unHideNode($secondPack[%cl.secondPack]);
		%pl.setNodeColor($secondPack[%cl.secondPack],%cl.secondPackColor);
	}
	if($hat[%cl.hat] !$= "none")
	{
		%pl.unHideNode($hat[%cl.hat]);
		%pl.setNodeColor($hat[%cl.hat],%cl.hatColor);
	}
	if(%cl.hip)
	{
		%pl.unHideNode("skirthip");
		%pl.unHideNode("skirttrimleft");
		%pl.unHideNode("skirttrimright");
	}
	else
	{
		%pl.unHideNode("pants");
		%pl.unHideNode((%cl.rleg ? "rpeg" : "rshoe"));
		%pl.unHideNode((%cl.lleg ? "lpeg" : "lshoe"));
	}
	%pl.setHeadUp(0);
	if(%cl.pack+%cl.secondPack > 0)
		%pl.setHeadUp(1);
	if($hat[%cl.hat] $= "Helmet")
	{
		if(%cl.accent == 1)
		{
			%pl.unHideNode($accent[4]);
			%pl.setNodeColor($accent[4],%cl.accentColor);
		}
	}
	else if(strpos($accentsAllowed[$hat[%cl.hat]],strlwr($accent[%cl.accent])) != -1)
	{
		%pl.unHideNode($accent[%cl.accent]);
		%pl.setNodeColor($accent[%cl.accent],%cl.accentColor);
	}

	if (%pl.bloody["lshoe"])
		%pl.unHideNode("lshoe_blood");
	if (%pl.bloody["rshoe"])
		%pl.unHideNode("rshoe_blood");
	if (%pl.bloody["lhand"])
		%pl.unHideNode("lhand_blood");
	if (%pl.bloody["rhand"])
		%pl.unHideNode("rhand_blood");
	if (%pl.bloody["chest_front"])
		%pl.unHideNode((%cl.chest ? "fem" : "") @ "chest_blood_front");
	if (%pl.bloody["chest_back"])
		%pl.unHideNode((%cl.chest ? "fem" : "") @ "chest_blood_back");

	%pl.setFaceName(%cl.faceName);
	%pl.setDecalName(%cl.decalName);
	
	%pl.setNodeColor("headskin",%cl.headColor);
	
	%pl.setNodeColor("chest",%cl.chestColor);
	%pl.setNodeColor("femChest",%cl.chestColor);
	%pl.setNodeColor("pants",%cl.hipColor);
	%pl.setNodeColor("skirthip",%cl.hipColor);
	
	%pl.setNodeColor("rarm",%cl.rarmColor);
	%pl.setNodeColor("larm",%cl.larmColor);
	%pl.setNodeColor("rarmSlim",%cl.rarmColor);
	%pl.setNodeColor("larmSlim",%cl.larmColor);
	
	%pl.setNodeColor("rhand",%cl.rhandColor);
	%pl.setNodeColor("lhand",%cl.lhandColor);
	%pl.setNodeColor("rhook",%cl.rhandColor);
	%pl.setNodeColor("lhook",%cl.lhandColor);
	
	%pl.setNodeColor("rshoe",%cl.rlegColor);
	%pl.setNodeColor("lshoe",%cl.llegColor);
	%pl.setNodeColor("rpeg",%cl.rlegColor);
	%pl.setNodeColor("lpeg",%cl.llegColor);
	%pl.setNodeColor("skirttrimright",%cl.rlegColor);
	%pl.setNodeColor("skirttrimleft",%cl.llegColor);

	//Set blood colors.
	%pl.setNodeColor("lshoe_blood", "0.7 0 0 1");
	%pl.setNodeColor("rshoe_blood", "0.7 0 0 1");
	%pl.setNodeColor("lhand_blood", "0.7 0 0 1");
	%pl.setNodeColor("rhand_blood", "0.7 0 0 1");
	%pl.setNodeColor("chest_blood_front", "0.7 0 0 1");
	%pl.setNodeColor("chest_blood_back", "0.7 0 0 1");
	%pl.setNodeColor("femchest_blood_front", "0.7 0 0 1");
	%pl.setNodeColor("femchest_blood_back", "0.7 0 0 1");
}

package playerMeleePackage
{
	function gameConnection::applyBodyColors(%cl,%o) 
	{
		parent::applyBodyColors(%cl,%o);
		if(isObject(%pl = %cl.player))
			if((%pl.getDatablock()).shapeFile $= "base/data/shapes/player/mmelee.dts")
				%pl.fixAppearance(%cl);
	}
	function gameConnection::applyBodyParts(%cl,%o) 
	{
		parent::applyBodyParts(%cl,%o);
		if(isObject(%pl = %cl.player))
			if((%pl.getDatablock()).shapeFile $= "base/data/shapes/player/mmelee.dts")
				%pl.fixAppearance(%cl);
	}
};
activatePackage(playerMeleePackage);