const MongoClient = require('mongodb').MongoClient;
const express = require('express');
const mongoose = require('mongoose');
const bcrypt = require('bcrypt');
const app = express(); //Now we can make routes

const postRoutes = require('./routes/locationPost');

const uri = "mongodb+srv://master:pass@main.jzhkr.mongodb.net/<dbname>?retryWrites=true&w=majority";
const client = new MongoClient(uri, { useNewUrlParser: true, useUnifiedTopology: true });
client.connect(err => {
  const collection = client.db("test").collection("devices");
  console.log('hi')
  client.close();
});

app.post('/users', async (req,res) => {
    try{
        const salt = await bcrypt.genSalt()
        const hashedPassword = await bcrypt.hash(req.body.password,salt)
        console.log(salt)
        console.log(hashedPassword)
        const user = {name: req.body.name, password: hashedPassword}
        users.push(user)
        res.status(201).send()
    } catch{
        res.status(500).send()
    }
    
})

app.post('/users/login', async (req, res) => {
    const user = users.find(user => user.name === req.body.name)
    if (user == null) {
      return res.status(400).send('Cannot find user')
    }
    try {
      if(await bcrypt.compare(req.body.password, user.password)) {
        res.send('Success')
      } else {
        res.send('Not Allowed')
      }
    } catch {
      res.status(500).send()
    }
  })




app.listen(3000)