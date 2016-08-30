﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Player {
  public Image panel;
  public Text text;
  public Button button;
}

[System.Serializable]
public class PlayerColor {
  public Color panelColor;
  public Color textColor;
}

public class GameController : MonoBehaviour {

  public Text[] buttonList;
  public GameObject gameOverPanel;
  public Text gameOverText;
  public GameObject restartButton;
  public GameObject startInfo;

  public Player playerX;
  public Player playerO;
  public PlayerColor activePlayerColor;
  public PlayerColor inactivePlayerColor;

  private string playerSide;
  private int moveCount;

  void Awake() {
    SetGameControllerReferenceOnButtons();

    moveCount = 0;

    gameOverPanel.SetActive(false);
    restartButton.SetActive(false);
    startInfo.SetActive(true);
  }

  void SetGameControllerReferenceOnButtons() {
    for (int i = 0; i < buttonList.Length; i++) {
      buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
    }
  }

  public string GetPlayerSide() {
    return playerSide;
  }

  void ChangeSides() {
    if (playerSide == "X") {
      playerSide = "O";
      SetPlayerColors(playerO, playerX);
    } else {
      playerSide = "X";
      SetPlayerColors(playerX, playerO);
    }
  }

  public void EndTurn() {

    moveCount++;

    if (buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide) {
      GameOver(playerSide);
    }

    else if (buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide) {
      GameOver(playerSide);
    }

    else if (buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide) {
      GameOver(playerSide);
    }

    else if (buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide) {
      GameOver(playerSide);
    }

    else if (buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [7].text == playerSide) {
      GameOver(playerSide);
    }

    else if (buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide) {
      GameOver(playerSide);
    }

    else if (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide) {
      GameOver(playerSide);
    }

    else if (buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide) {
      GameOver(playerSide);
    }

    else if (moveCount > 8) {
      GameOver("draw");
    }

    else {
      ChangeSides();
    }

  }

  void SetGameOverText(string text) {
    gameOverPanel.SetActive(true);
    gameOverText.text = text;
  }

  void SetBoardInteractable(bool toggle) {
    for (int i = 0; i < buttonList.Length; i++) {
      buttonList[i].GetComponentInParent<Button>().interactable = toggle;
    }
  }

  void SetPlayerColors(Player newPlayer, Player oldPlayer) {
    newPlayer.panel.color = activePlayerColor.panelColor;
    newPlayer.text.color = activePlayerColor.textColor;
    oldPlayer.panel.color = inactivePlayerColor.panelColor;
    oldPlayer.text.color = inactivePlayerColor.textColor;
  }

  void SetPlayerColorsInactive() {
    playerX.panel.color = inactivePlayerColor.panelColor;
    playerX.text.color = inactivePlayerColor.textColor;
    playerO.panel.color = inactivePlayerColor.panelColor;
    playerO.text.color = inactivePlayerColor.textColor;
  }

  void SetPlayerButtons(bool toggle) {
    playerX.button.interactable = toggle;
    playerO.button.interactable = toggle;
  }

  public void SetStartingSide(string startingSide) {
    playerSide = startingSide;
    if (playerSide == "X") {
      SetPlayerColors(playerX, playerO);
    } else {
      SetPlayerColors(playerO, playerX);
    }
      
    StartGame();
  }

  void StartGame() {
    SetBoardInteractable(true);
    SetPlayerButtons(false);
    startInfo.SetActive(false);
  }
    
  void GameOver(string winningPlayer) {

    SetBoardInteractable(false);

    if (winningPlayer == "draw") {
      SetGameOverText("It's a draw!");
      SetPlayerColorsInactive();
    } else {
      SetGameOverText(winningPlayer + " Wins!");
    }

    restartButton.SetActive(true);

  }

  public void RestartGame() {
    moveCount = 0;

    gameOverPanel.SetActive(false);
    restartButton.SetActive(false);
    startInfo.SetActive(true);

    for (int i = 0; i < buttonList.Length; i++) {
      buttonList[i].text = "";
    }

    SetPlayerButtons(true);
    SetPlayerColorsInactive();
  }
    
}