import axios from "axios";
import { baseJobUrl } from "../constants/urlConstants";

const httpModule = axios.create({
    baseURL: baseJobUrl
});

export default httpModule;