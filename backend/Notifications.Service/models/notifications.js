import mongoose from 'mongoose';

const { Schema } = mongoose;

const NotificationSchema = new Schema({
  _id: {
    type: Schema.Types.ObjectId,
    auto: true,
  },
  description: {
    type: String,
    required: true,
  },
  timeCreated: {
    type: Date,
    default: Date.now,
  },
});

export default mongoose.model('Notification', NotificationSchema);
