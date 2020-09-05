const MongoClient = require('mongodb').MongoClient;
const express = require('express');
const mongoose = require('mongoose');
const app = express(); //Now we can make routes

const postRoutes = require('./routes/locationPost');

const uri = "mongodb+srv://master:pass@main.jzhkr.mongodb.net/<dbname>?retryWrites=true&w=majority";
const client = new MongoClient(uri, { useNewUrlParser: true, useUnifiedTopology: true });
client.connect(err => {
  const collection = client.db("test").collection("devices");
  console.log('hi')
  client.close();
});

app.use('/location',postRoutes)


app.listen(3000)