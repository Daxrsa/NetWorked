import express from 'express'
import User from '../models/User.js'
import dotenv from 'dotenv';
import amqp from 'amqplib';
dotenv.config();

const router= express.Router();
router.use(express.json());
router.post('/', async (req, res) => {
    try {
      // ...
  
      // ...
  
      // Connect to RabbitMQ and consume the message
      const connection = await amqp.connect('amqp://localhost');
      const channel = await connection.createChannel();
  
      const queueName = 'user_service';
  
      // Consume messages from the queue
      await channel.consume(queueName, async (msg) => {
        // Process the received message
        const receivedMessage = JSON.parse(msg.content.toString());
       
        // Your code to process the message and save it to the database
        // Assuming you have a User model and want to save the received message as a new user
        const username = receivedMessage.Username;
        const newUser = new User({
          username
        });
        await newUser.save();
  
        // Set the response JSON with only Username
        res.status(201).json(username);
  
        // Acknowledge the message to remove it from the queue
        channel.ack(msg);
      });
  
      // ...
    } catch (error) {
      res.status(500).json({ error: error.message });
    }
  });
  router.get("/", async (req, res) => {
    try {
      let users = await User.find().select("username");
  
      res.json({
        users: users.map(user => user.username)
      });
    } catch (err) {
      res.status(500).json({
        success: false,
        message: err.message
      });
    }
  });
  
  export default router;