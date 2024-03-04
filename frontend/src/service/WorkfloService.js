

import Axios from "axios";
export default {
    UploadFile(file){
        const formData = new FormData();
        formData.append('file', file);
        return Axios.post("http://localhost:5182/api/bpmn",formData);
    }
    
}