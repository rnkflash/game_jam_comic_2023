using UnityEngine;
using System;

public class Characters : Singleton<Characters>
{
	private CharacterCard[] characterCards;
	
	protected override void Created()
	{
		base.Created();
		characterCards = Resources.LoadAll<CharacterCard>("Characters");

	}

	public CharacterCard GetCharacterCard(Character character)
	{
		return Array.Find(characterCards, element => element.id == character);
	}
}