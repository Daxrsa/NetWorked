import axios from "axios";
import { baseUserUrl } from "../constants/userUrl";

const httpUserUrl = axios.create({
    userUrl: baseUserUrl
});

export default httpUserUrl;