using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class MainSystem : GenericSingleton<MainSystem> { //data field
}
public partial class MainSystem : GenericSingleton<MainSystem> { //data field

    private void Allocate() {
        // DataManager = gameObject.AddComponent<DataManager>();
        // ParserManager = gameObject.AddComponent<ParserManager>();
    }
    public void Initialize() {
        Allocate();
        // ParserManager.Initialize();
        // DataManager.Initialize();
    }

    public void ActiveMainSystem() {
        Initialize();
        // SceneManager.LoadScene("NatureQuiz");
    }
}