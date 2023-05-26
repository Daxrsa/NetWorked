import express from 'express'
import Notification from '../models/notifications.js'

const router= express.Router();
router.use(express.json());

router.get("/",(req,res)=>{
    res.send("notifications")
})
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



  
export default router;
