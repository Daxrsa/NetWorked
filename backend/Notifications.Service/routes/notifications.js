import express from 'express'
import Notification from '../models/notifications.js'

const router= express.Router();
router.use(express.json());


router.post('/', async (req, res) => {
  try {
    const newNotification = {
      description: req.body.description,
    };

    // Create the notification in the database
    const createdNotification = await Notification.create(newNotification);

    res.status(201).json(createdNotification);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});
//GET API
router.get("/",async(req,res)=>{
  try{
    let notifications = await Notification.find();
    const descriptions = notifications.map(notification => notification.description);
res.json({
  descriptions
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







  
export default router;
