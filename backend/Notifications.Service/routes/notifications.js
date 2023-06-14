import express from 'express'
import Notification from '../models/notifications.js'
import algoliaSearch from 'algoliasearch'
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

    const queueName = 'notifications_service';

    // Consume messages from the queue
    await channel.consume(queueName, (msg) => {
      // Process the received message
      const receivedMessage = JSON.parse(msg.content.toString());
      const { Username, Description } = receivedMessage;
      console.log('Received message:', receivedMessage);

      // Set the response JSON with only Username and Description
      res.status(201).json({ username: Username, description: Description });

      // Your code to process the message and save it to the database
      // ...

      // Acknowledge the message to remove it from the queue
      channel.ack(msg);
    });

    // ...
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});



//GET API
router.get("/",async(req,res)=>{
  try{
    let notifications = await Notification.find();
    const mappedData = notifications.map(notification => {
      return {
        
        username: notification.username,
        description: notification.description
      };
    });
    
res.json({
  mappedData
});
  }catch(err){
      res.status(500).json({
          success:false,
          message:err.message
      })
     
  } 
});
//GET request - get a single notification
router.get("/:id",async(req,res)=>{
  try{
let notification=await Notification.findOne({_id:req.params.id});
res.json({
  notification:notification
});
  }catch(err){
      res.status(500).json({
          success:false,
          message:err.message
      })
     
  } 
});
//PUT request -update a single notification
router.put("/:id",async(req,res)=>{
  try{
let notification=await Notification.findOneAndUpdate(
  {_id:req.params.id},
  {
  $set:{
      description:req.body.description,
     }
  },
  {upsert:true}
);
res.json({
  updatedNotification:notification
});
  }catch(err){
      res.status(500).json({
          success:false,
          message:err.message
      })
     
  } 
});
//DELETE request-  delete a single notification
router.delete("/:id",async(req,res)=>{
  try {
   let deletedNotification=await Notification.findOneAndDelete({_id:req.params.id });
   if(deletedNotification){
      res.json({
          status:true,
          message:"Successfully deleted"
      })
   }
  }catch(err){
      res.status(500).json({
          success:false,
          message:err.message
      })
     
  } 
})

router.get('/description/:id', async (req, res) => {
  try {
    const notification = await Notification.findOne({ _id: req.params.id }, 'description');
    if (!notification) {
      return res.status(404).json({ success: false, message: 'Notification not found' });
    }
    res.json({ description: notification.description });
  } catch (error) {
    res.status(500).json({ success: false, message: error.message });
  }
});

const client = algoliaSearch(
  process.env.ALGOLIA_APP_ID,
  process.env.ALGOLIA_API_KEY
);
const index = client.initIndex(process.env.ALGOLIA_INDEX);

router.post("/search", async (req, res) => {
  try {
    let result = await index.search(req.body.username); 
    res.json(result.hits);
  } catch (err) {
    res.status(500).json({
      status: false,
      message: err.message,
    });
  }
});







  
export default router;
