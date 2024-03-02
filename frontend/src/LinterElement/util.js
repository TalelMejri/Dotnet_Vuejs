export var errors=[];

export function addError(error,element_id) {
    if(errors.find(e => e.id === element_id) === undefined){
        errors.push({id:element_id, error: error});
    }
}

export function removeError(element_id) {
    const index = errors.findIndex(e => e.id === element_id);
    if(index !== -1){
        errors.splice(index, 1);
    }
}
