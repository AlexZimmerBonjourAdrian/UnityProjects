// Fill out your copyright notice in the Description page of Project Settings.
#include "BullCowCartridge.h"

FString HiddentWorld;
int32 Lives = 3;
void UBullCowCartridge::BeginPlay() // When the game starts
{
	Super::BeginPlay();
	//Welcoming the eye
	PrintLine(TEXT("Welcome to Bull Cows!"));
	PrintLine(TEXT("Guess the 4 letter world!")); // Magic Number Remove!
	PrintLine(TEXT("Press enter to continue...."));

	SetupGame();// Setting Up Game
	//HiddentWorld = TEXT("cake"); // Mover Fuera de esta funcion.
	// Set Lives

	//Prompt Player For Guess
}

void UBullCowCartridge::OnInput(const FString& Input) // When the player hits enter
{
	ClearScreen();

	//HiddentWorld.
	// Checking PlayGuess

	PrintLine(Input);

	if (Input == HiddentWorld)
		{
			PrintLine(TEXT("You have Won!"));
		}
	else
	{
			if(Input.Len() != HiddentWorld.Len())
			{ 
				PrintLine(TEXT("The Hidden World is 4 Character long, try again!"));
			}

			PrintLine(TEXT("You Have Lost!"));
	}

	// Check If Isogram
	//Promt To Guess Again
	//Check Right Number of Cahracters
	//Prompt To Guess Again

	//Remove Life

	// Check If Lives > 0
	// If Yes GuessAgain
	// Show Lives Left
	// If Not Show GameOver and HiddenWord?
	// Prompt to play again, Press Enter to Play Again?
	// Check Use Input
	// PlayAgain Or Quit
}

void UBullCowCartridge::SetupGame()
{
	HiddentWorld = TEXT("cake");
	Lives = 4; // Set Lives
}
