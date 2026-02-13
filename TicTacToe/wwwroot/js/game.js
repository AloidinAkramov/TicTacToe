const connection = new signalR.HubConnectionBuilder()
    .withUrl("/gamehub", {
        transport: signalR.HttpTransportType.LongPolling
    })
    .build();

const cells = document.querySelectorAll(".cell");
const statusDiv = document.getElementById("status");

cells.forEach(cell => cell.disabled = true);

connection.start()
    .then(() => {
        console.log("Connected");
        connection.invoke("JoinGame", GAME_ID);
    });

connection.on("GameStarted", (currentTurn) => {

    if (currentTurn === PLAYER_NAME) {
        statusDiv.innerText = "Your turn";
        cells.forEach(c => c.disabled = false);
    } else {
        statusDiv.innerText = "Opponent's turn";
    }
});

connection.on("MoveMade", (board, currentTurn) => {

    updateBoard(board);

    if (currentTurn === PLAYER_NAME) {
        statusDiv.innerText = "Your turn";
        board.forEach((v, i) => {
            if (v === null) cells[i].disabled = false;
        });
    } else {
        statusDiv.innerText = "Opponent's turn";
        cells.forEach(c => c.disabled = true);
    }
});

connection.on("GameOver", (board, winCombo, winnerName, playerXName, playerOName, xScore, oScore) => {

    updateBoard(board);

    if (PLAYER_NAME === playerXName)
    {
        updateScore(xScore, oScore);
    }
    else
    {
        updateScore(oScore, xScore);
    }


    winCombo.forEach(i => {
        cells[i].classList.add("win");
    });

    if (winnerName === PLAYER_NAME) {
        statusDiv.innerText = "🎉 YOU WIN!";
    } else {
        statusDiv.innerText = "💀 YOU LOSE!";
    }

    cells.forEach(c => c.disabled = true);

    showRestartButton();
});

connection.on("GameDraw", (board) => {

    updateBoard(board);
    statusDiv.innerText = "🤝 DRAW!";
    cells.forEach(c => c.disabled = true);

    showRestartButton();
});

connection.on("GameRestarted", (board, currentTurn) => {

    updateBoard(board);

    cells.forEach(c => {
        c.classList.remove("win");
        c.disabled = false;
    });

    if (currentTurn === PLAYER_NAME) {
        statusDiv.innerText = "Your turn";
    } else {
        statusDiv.innerText = "Opponent's turn";
        cells.forEach(c => c.disabled = true);
    }

    const btn = document.getElementById("restartBtn");
    if (btn) btn.remove();
});

function updateBoard(board) {
    board.forEach((value, index) => {
        cells[index].innerText = value ?? "";
        cells[index].disabled = value !== null;
    });
}

cells.forEach(cell => {
    cell.addEventListener("click", () => {

        if (cell.disabled) return;

        // IMPORTANT: Send move to server via SignalR
        connection.invoke(
            "MakeMove",
            GAME_ID,
            parseInt(cell.dataset.index),
            PLAYER_NAME
        );
    });
});

function showRestartButton() {

    if (document.getElementById("restartBtn")) return;

    const restartBtn = document.createElement("button");
    restartBtn.id = "restartBtn";
    restartBtn.innerText = "🔁 Play Again";
    restartBtn.className = "game-btn";
    restartBtn.style.marginTop = "20px";

    restartBtn.onclick = () => {
        connection.invoke("RestartGame", GAME_ID);
    };

    document.querySelector(".game-container")
        .appendChild(restartBtn);
}

function updateScore(x, o) {
    document.getElementById("playerXScore").innerText = x;
    document.getElementById("playerOScore").innerText = o;
}