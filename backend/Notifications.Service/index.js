import express from 'express';
import dotenv from 'dotenv';
import mongoose from 'mongoose';
import amqp from 'amqplib';
import NotificationRoute from './routes/notifications.js';
import cors from 'cors';

const app = express();

dotenv.config();

const connect = async () => {
  try {
    await mongoose.connect(process.env.MONGO);
    console.log('MongoDB connected');

    const connection = await amqp.connect('amqp://localhost');
    const channel = await connection.createChannel();

    const exchangeName = 'my-exchange';
    const queueName = 'notifications_service';

    // Create exchange
    await channel.assertExchange(exchangeName, 'direct', { durable: true });

    // Consume messages from the queue
  

    console.log('RabbitMQ setup completed.');
    
  } catch (error) {
    console.error('Error setting up RabbitMQ:', error);
  }
};

app.get('/', (req, res) => {
  res.send('testing');
});

// Middlewares
app.use(cors());
app.use('/notifications', NotificationRoute);

connect().then(() => {
  app.listen(8800, () => {
    console.log('Connected to backend!');
  });
});
