import mongoose from 'mongoose';

const {Schema} =mongoose;

const NotificationSchema = new mongoose.Schema({
  description: {
    type: String,
    required: true,
  },
  timeCreated: {
    type: Date,
    default: Date.now,
  },
});




export default mongoose.model("Notifications",NotificationSchema);
