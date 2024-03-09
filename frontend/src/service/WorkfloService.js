import Axios from "axios";
export default {
    UploadFile(file,data){
        const formData = new FormData();
        formData.append('file', file);
        formData.append('data', JSON.stringify(data));
        return Axios.post("http://localhost:5182/api/bpmn",formData);
    }
}