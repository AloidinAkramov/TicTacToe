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

connection.on("GameOver", (board, winCombo, winnerName) => {

    updateBoard(board);

    winCombo.forEach(i => {
        cells[i].classList.add("win");
    });

    if (winnerName === PLAYER_NAME) {
        statusDiv.innerText = "🎉 YOU WIN!";
    } else {
        statusDiv.innerText = "💀 YOU LOSE!";
    }

    cells.forEach(c => c.disabled = true);
});

connection.on("GameDraw", (board) => {

    updateBoard(board);
    statusDiv.innerText = "🤝 DRAW!";
    cells.forEach(c => c.disabled = true);
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

        connection.invoke(
            "MakeMove",
            GAME_ID,
            parseInt(cell.dataset.index),
            PLAYER_NAME
        );
    });
});