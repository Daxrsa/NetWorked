
import mongoose from 'mongoose';
import dotenv from 'dotenv';


dotenv.config();

const { Schema } = mongoose;

const UserSchema = new Schema({
_id: {
type: Schema.Types.ObjectId,
auto: true,
},
username:{
type:String,
required:true,
},
});



const UserModel = mongoose.model('User', UserSchema);


export default UserModel;