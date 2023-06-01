
import mongoose from 'mongoose';
import dotenv from 'dotenv';
import mongooseAlgolia from 'mongoose-algolia';

dotenv.config();

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

NotificationSchema.plugin(mongooseAlgolia, {
appId: process.env.ALGOLIA_APP_ID || '', // Provide a default value if ALGOLIA_APP_ID is not defined
apiKey: process.env.ALGOLIA_API_KEY || '', // Provide a default value if ALGOLIA_API_KEY is not defined
indexName: "NETWORKED",
selector: 'description',
debug: true,
});

const NotificationModel = mongoose.model('Notification', NotificationSchema);

NotificationModel.SyncToAlgolia(); // Clear the Algolia index and synchronize all documents
NotificationModel.SetAlgoliaSettings({
searchableAttributes: ["description"],
});

export default NotificationModel;