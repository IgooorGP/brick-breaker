﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.LevelMap;

public class LevelController : MonoBehaviour
{
    private readonly string GAME_OVER_SCENE_NAME = "Scenes/GameOver";
    private readonly int NUMBER_OF_GAME_LEVELS = 40; // ban đầu là 3
    
    // UI elements
    [SerializeField] int blocksCounter;

    // state
     private SceneLoader _sceneLoader;
    private LoadMap _csvLoader; //load map theo csv

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _csvLoader = FindObjectOfType<LoadMap>();
    }

    public void IncrementBlocksCounter() //tăng dần
    {
        blocksCounter++;
    }
    
    public void DecrementBlocksCounter() // giảm dần thằng này không ở hàm update mà ở script block
    {
        blocksCounter--;

        if (blocksCounter <= 0) //Nếu blocksCounter <0 hết block đễ phá => win game. tìm nó được thằng nào truyền vào
        {
            var gameSession = GameSession.Instance;// điên máu gọi nó đến và dùng nó mà không cần khai báo 
            
            // check for game over
            if (gameSession.GameLevel >= NUMBER_OF_GAME_LEVELS) //nếu chơi tới màn cuối cùng. tìm gamelevel
            {
                _sceneLoader.LoadSceneByName(GAME_OVER_SCENE_NAME); // thì load sence game over
            }

            // increases game level
            MapManager.Instance.SetVictory(gameSession.GameLevel);
            gameSession.GameLevel++;// tìm game level
        // _sceneLoader.LoadNextScene(); //tắt cái này là hok loadNextScene // nếu viết hàm load csv ở đây thì win
            _csvLoader.nextMap();
        }
    }
    
}
