import express from 'express'
import dotenv from 'dotenv'
import mongoose from 'mongoose'
import NotificationRoute from './routes/notifications.js'

const app= express ()

dotenv.config()
const connect = async () => {
    try {
        await mongoose.connect(process.env.MONGO)
        console.log("MongoDB connected")
    }catch(error){
        throw error;
    }
}

app.get("/",(req,res)=> {
    res.send("testing")
})
//middlewares
app.use("/notifications",NotificationRoute);

app.listen(8800,()=> {
    connect()
    console.log("Connected to backend!")
})

