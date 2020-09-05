const express = require("express")
const app = express()
const cors = require('cors')


// some basic database commands
const { getCollection, getPrimarykey, connect } = require("./database");




app.use(express.urlencoded({ extended: false }))
app.use(express.json())
app.use(cors())


// create a new android user
app.post("/createuser", (req, res)=>{
    const { androidID } = req.body;
    res.send("I did something, idk if it worked yet")
})


app.listen(5000)