<template>
    <div>
        <p>Neoledge Panel</p>
        <div class="header">
            <button @click="selected = 'prop'" :class="selected == 'prop' ? 'active' : ''">
                Properties
            </button>
            <button @click="selected = 'comment'" :class="selected == 'comment' ? 'active' : ''">
                Comments
            </button>
        </div>
        <div class="body mt-2" v-if="element != ''">
            <div v-if="selected == 'comment'" class="comment-section">
                <div v-if="Comments != ''">
                    <div v-for="(CommentValue,index) in Comments">
                        <div class="comment-header">
                            <span class="comment-user">{{ CommentValue.IdUser }}</span>
                            <!-- <span class="comment-date">12/05/02</span> -->
                        </div>
                        <div class="d-flex justify-content-between align-items-center comment-text">
                            <div class="p-2">
                                {{ CommentValue.comment }}
                            </div>
                            <div>
                                <button @click="deleteComment(index)" class="btn btn-danger">Delete</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div v-else>
                    <div class="no-comments py-5 mt-5">
                        NO Comments Yet
                    </div>
                </div>
                <div class="input-container">
                    <input type="text" v-model="commentInput" placeholder="Enter your comment...">
                    <button @click="addComments()">Add</button>
                </div>
            </div>
            <div v-else>
                <div class="NameContent">
                    <p  v-if="element[2].split(':')[0] !='bpmn'"> 
                       <span class="text" v-if="element[3]['eventDefinitions']!=undefined"> Name : {{ element[3]['eventDefinitions'][0]["$type"].split(':')[1] }}</span>
                       <span class="text" v-else>  Name : {{ element[3]["$type"].split(':')[1] }}</span>
                    </p>
                    <p v-else >
                        <span  class="text">Name : </span>  {{ element[2].split(':')[1]  }}
                    </p>
                </div>
                <div class="mt-2">
                    <div class="accordion" id="accordionExample">
                        <div class="accordion-item mb-1">
                            <h2 class="accordion-header" id="headingOne">
                                <button class="accordion-button" type="button" data-bs-toggle="collapse"
                                    data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                    General
                                </button>
                            </h2>
                            <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne"
                                data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <input class="form-control mb-2" v-model="name" @input="ChangeName" type="text"
                                        placeholder="Name" />
                                    <input class="form-control" v-model="id" type="text" placeholder="id" />
                                </div>
                            </div>
                        </div>

                        <div class="accordion-item" v-if="element[2].split(':')[1] === 'SendTask'">
                            <h2 class="accordion-header" id="headingTask">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                    data-bs-target="#taskDefinition" aria-expanded="false" aria-controls="taskDefinition">
                                    Task Definition
                                </button>
                            </h2>
                            <div id="taskDefinition" class="accordion-collapse collapse " aria-labelledby="headingTask"
                                data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <input class="form-control mb-2" v-model="TypeFx" type="text" placeholder="Type Fx" />
                                    <input class="form-control" v-model="Retries" type="text" placeholder="Retries" />
                                    <button @click="AddFn()">Add FN</button>
                                </div>
                            </div>
                        </div>

                        <div class="accordion-item" v-if="element[3]['eventDefinitions']!=undefined && element[3]['eventDefinitions'][0]['$type'].split(':')[1]=='TimerEventDefinition'">
                            <h2 class="accordion-header" id="headingTimer">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                    data-bs-target="#taskTimer" aria-expanded="false" aria-controls="taskTimer">
                                        Timer
                                </button>
                            </h2>
                            <div id="taskTimer" class="accordion-collapse collapse " aria-labelledby="headingTimer"
                                data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <input :class="Timer!='' ? 'form-control mb-2' :'form-control mb-2 is-invalid'" v-model="Timer" type="text" placeholder="Add Time With Minute" />
                                    <button @click="AddTimer()">Add Timer</button>
                                </div>
                            </div>
                        </div>

                        <div class="accordion-item" v-if="element[2].split(':')[1] === 'SendTask'">
                            <h2 class="accordion-header" id="headingHeaders">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                    data-bs-target="#headersDefinition" aria-expanded="false"
                                    aria-controls="headersDefinition">
                                    Headers
                                </button>
                            </h2>
                            <div id="headersDefinition" class="accordion-collapse collapse "
                                aria-labelledby="headingHeaders" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="content_prop">
                                        <div class="card  mb-2" v-for="(header, index) in AllHeaders" :key="index">
                                            <p>Key : {{ header.key }}</p>
                                            <p>Value : {{ header.value }}</p>
                                            <div class="d-flex gap-2 justify-content-center mb-2">
                                                <button class="btn btn-danger" @click="deleteHeader(index)">Delete</button>
                                                <button class="btn btn-warning"
                                                    @click="showEditHeader(index, header.key, header.value)">
                                                    Edit</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="formAdd ">
                                        <p>Add Headers :</p>
                                        <input type="text" class="form-control mb-2" placeholder="Key" v-model="key">
                                        <input type="text" class="form-control  mb-2" placeholder="Value"
                                            v-model="value_header">
                                        <button v-if="showHeadersEdit" class="btn btn-warning"
                                            @click="updatePropertyHeader()">Edit</button>
                                        <button v-else class="btn btn-primary" @click="AddHeaders()">Add</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="accordion-item" v-if="element[2].split(':')[1] === 'ScriptTask'">
                            <h2 class="accordion-header" id="headingPython">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                    data-bs-target="#headerPython" aria-expanded="false" aria-controls="headerPython">
                                    Code Python
                                </button>
                            </h2>
                            <div id="headerPython" class="accordion-collapse collapse " aria-labelledby="headingPython"
                                data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="content_prop">
                                        <div class="card  mb-2">
                                            <div class="card-body">
                                                <textarea
                                                    :class="error_code == '' && code_python !='' ? 'form-control' : 'form-control is-invalid'"
                                                    name="text" placeholder="Enter Your Code Here" v-model="code_python"
                                                  ></textarea>
                                                  <!-- @input="checkCode()" -->
                                                <!-- <small v-if="error_code != ''" class="text-danger">{{ error_code }}</small> -->
                                            </div>
                                            <div class="d-flex gap-2 justify-content-center mb-2">
                                                <button class="btn btn-primary" @click="AddCode()">Add</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="accordion-item" v-if="element[2].split(':')[1] === 'SendTask'">
                            <h2 class="accordion-header" id="headingInputs">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                    data-bs-target="#headerInput" aria-expanded="false" aria-controls="headerInput">
                                    Inputs
                                </button>
                            </h2>
                            <div id="headerInput" class="accordion-collapse collapse " aria-labelledby="headingInputs"
                                data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="content_prop">
                                        <div class="card  mb-2" v-for="(input, index) in AllInputs" :key="index">
                                            <p>Local Name : {{ input.name }}</p>
                                            <p>Variable Fx : {{ input.value }}</p>
                                            <div class="d-flex gap-2 justify-content-center mb-2">
                                                <button class="btn btn-danger" @click="DeleteInput(index)">Delete</button>
                                                <button class="btn btn-warning"
                                                    @click="ShowEditInput(index, input.name, input.value)">
                                                    Edit</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="formAdd">
                                        <p>Add Input :</p>
                                        <input type="text" class="form-control mb-2" placeholder="localName"
                                            v-model="localName">
                                        <input type="text" class="form-control  mb-2" placeholder="variable_fx"
                                            v-model="variable_fx">
                                        <button v-if="showInputEdit" class="btn btn-warning"
                                            @click="UpdatePropertyInput()">Edit</button>
                                        <button v-else class="btn btn-primary" @click="AddInputs()">Add</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="accordion-item" v-if="element[2].split(':')[1] === 'SendTask'">
                            <h2 class="accordion-header" id="headingOutput">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                    data-bs-target="#headerOutput" aria-expanded="false" aria-controls="headerOutput">
                                    Outputs
                                </button>
                            </h2>
                            <div id="headerOutput" class="accordion-collapse collapse " aria-labelledby="headingOutput"
                                data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="content_prop">
                                        <div class="card  mb-2" v-for="(Output, index) in AllOutputs" :key="index">
                                            <p>Local Name : {{ Output.name }}</p>
                                            <p>Variable Fx : {{ Output.value }}</p>
                                            <div class="d-flex gap-2 justify-content-center mb-2">
                                                <button class="btn btn-danger" @click="DeleteOutput(index)">Delete</button>
                                                <button class="btn btn-warning"
                                                    @click="ShowEditOutput(index, Output.name, Output.value)">
                                                    Edit</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="formAdd">
                                        <p>Add OutPut :</p>
                                        <input type="text" class="form-control mb-2" placeholder="localName"
                                            v-model="localNameOutPut">
                                        <input type="text" class="form-control  mb-2" placeholder="variable_fx"
                                            v-model="variable_fxOutput">
                                        <button v-if="showOutPutEdit" class="btn btn-warning"
                                            @click="UpdatePropertyOutput()">Edit</button>
                                        <button v-else class="btn btn-primary" @click="AddOutputs()">Add</button>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="accordion-item">
                            <h2 class="accordion-header" id="headingTwo">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse"
                                    data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    Extension properties <span class="badge"> {{ AllProperties.length > 0 ?
                                        AllProperties.length : '' }}</span>
                                </button>
                            </h2>
                            <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo"
                                data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <div class="content_prop">
                                        <div class="card  mb-2" v-for="(prop, index) in AllProperties" :key="index">
                                            <p>Name : {{ prop.name }}</p>
                                            <p>Value : {{ prop.value }}</p>
                                            <div class="d-flex gap-2 justify-content-center mb-2">
                                                <button class="btn btn-danger"
                                                    @click="deleteProperty(index)">Delete</button>
                                                <button class="btn btn-warning"
                                                    @click="showEditModal(index, prop.name, prop.value)">
                                                    Edit</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="formAdd ">
                                        <p>Add New Propertie :</p>
                                        <input type="text" class="form-control mb-2" placeholder="name" v-model="name_form">
                                        <input type="text" class="form-control  mb-2" placeholder="value"
                                            v-model="value_form">
                                        <button v-if="showEdit" class="btn btn-warning"
                                            @click="updateProperty()">Edit</button>
                                        <button v-else class="btn btn-primary" @click="AddProperties()">Add</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

import { toRaw } from 'vue';
import {
    getComments,
    removeComment,
    addComment
} from '../comment_custom_copie/util.js';

export default {
    props: {
        element: Array
    },
    mounted() {
        this.InitMethods();
    },
    methods: {
        InitMethods() {
            if (this.element[0] != null) {
                this.getCodePython();
                this.getTimer();
                this.getAllComments();
                this.getAllProperties();
                this.getTaskDefinition();
                this.getAllHeaders();
                this.getAllInputs();
                this.getAllOutputs();
                this.getComments();
            }
        },
        getAllComments() {
            this.AllComments = [];
            this.commentInput = "";
            this.AllComments = getComments(this.element);
            if (this.element[3]['extensionElements'] != undefined) {
                if (this.element[3]['extensionElements']['values'] != undefined) {
                    let test = toRaw(this.element[3]['extensionElements']['values'].find((e) => e.$type == 'neo:Properties'));
                    if (test) {
                        let tab = test['properties'];
                        tab.forEach((val) => {
                            this.AllProperties.push({ name: val.name, value: val.value })
                        })
                    }
                }
            }
        },
        getAllHeaders() {
            this.AllHeaders = [];
            if (this.element[3]['extensionElements'] != undefined) {
                if (this.element[3]['extensionElements']['values'] != undefined) {
                    let test = toRaw(this.element[3]['extensionElements']['values'].find((e) => e.$type == 'neo:TaskHeaders'));
                    if (test) {
                        let tab = test['values'];
                        tab.forEach((val) => {
                            this.AllHeaders.push({ key: val.key, value: val.value })
                        })
                    }
                }
            }
        },
        getAllInputs() {
            this.AllInputs = [];
            if (this.element[3]['extensionElements'] != undefined) {
                if (this.element[3]['extensionElements']['values'] != undefined) {
                    let test = toRaw(this.element[3]['extensionElements']['values'].find((e) => e.$type == 'neo:IoMapping'));
                    if (test) {
                        let tab = test['inputParameters'];
                        if (tab) {
                            tab.forEach((val) => {
                                this.AllInputs.push({ name: val.source, value: val.target })
                            })
                        }
                    }
                }
            }
        },
        getCodePython(){
            if (this.element[3]['extensionElements'] != undefined) {
                if (this.element[3]['extensionElements']['values'] != undefined) {
                    let test = toRaw(this.element[3]['extensionElements']['values'].find((e) => e.$type == 'neo:PythonCode'));
                    if (test) {
                        this.code_python = test['code']
                    }
                }
            }
        },
        getTimer(){
            if (this.element[3]['extensionElements'] != undefined) {
                if (this.element[3]['extensionElements']['values'] != undefined) {
                    let test = toRaw(this.element[3]['extensionElements']['values'].find((e) => e.$type == 'neo:TimerCycle'));
                    if (test) {
                        this.Timer = test['time']
                    }
                }
            }
        },
        getTaskDefinition() {
            this.TypeFx = "";
            this.Retries = ""
            if (this.element[3]['extensionElements'] != undefined) {
                if (this.element[3]['extensionElements']['values'] != undefined) {
                    let test = toRaw(this.element[3]['extensionElements']['values'].find((e) => e.$type == 'neo:TaskDefinition'));
                    if (test) {
                        this.TypeFx = test['type']
                        this.Retries = test['retries']
                    }
                }
            }
        },
        getAllOutputs() {
            this.AllOutputs = [];
            if (this.element[3]['extensionElements'] != undefined) {
                if (this.element[3]['extensionElements']['values'] != undefined) {
                    let test = toRaw(this.element[3]['extensionElements']['values'].find((e) => e.$type == 'neo:IoMapping'));
                    if (test) {
                        let tab = test['outputParameters'];
                        if (tab) {
                            tab.forEach((val) => {
                                this.AllOutputs.push({ name: val.source, value: val.target })
                            })
                        }
                    }
                }
            }
        },
        getComments() {
            this.Comments = [];
            if (this.element[3]['extensionElements'] != undefined) {
                if (this.element[3]['extensionElements']['values'] != undefined) {
                    let test = toRaw(this.element[3]['extensionElements']['values'].find((e) => e.$type == 'neo:CommentTask'));
                    if (test) {
                        let tab = test['comments'];
                        if (tab) {
                            tab.forEach((val) => {
                                this.Comments.push({ IdUser: val.IdUser, comment: val.comment })
                            })
                        }
                    }
                }
            }
        },
        addComments() {
            this.$emit("SetComments",this.commentInput,2);
            //addComment(this.element, '', this.commentInput);
            this.InitMethods();
        },
        deleteComment(index) {
            this.Comments.splice(index, 1);
            this.$emit('deleteComment', this.Comments);
            this.InitMethods();
        },
        getAllProperties() {
            this.id = this.element[0];
            this.name = this.element[3]["name"]
        },
        ChangeName() {
            this.$emit('updateActivityName', this.name);
        },
        AddProperties() {
            this.$emit('setValue', this.name_form, this.value_form);
            this.InitMethods();
        },
        AddHeaders() {
            this.$emit('SetHeaders', this.key, this.value_header);
            this.InitMethods();
        },
        deleteHeader(index) {
            this.AllHeaders.splice(index, 1);
            this.$emit('deleteHeader', this.AllHeaders);
            this.InitMethods();
        },
        showEditHeader(index, name, value) {
            this.index = index;
            this.key = name;
            this.value_header = value;
            this.showHeadersEdit = true;
        },
        UpdatePropertyInput() {
            let OldName = this.AllInputs[this.index].name;
            let OldValue = this.AllInputs[this.index].value;
            this.AllInputs[this.index].name = this.localName;
            this.AllInputs[this.index].value = this.variable_fx;
            this.$emit("UpdatePropertyInput", this.localName, this.variable_fx, OldName, OldValue);
        },
        AddInputs() {
            this.$emit('AddInputs', this.localName, this.variable_fx);
            this.InitMethods();
        },
        DeleteInput(index) {
            this.AllInputs.splice(index, 1);
            this.$emit('DeleteInput', this.AllInputs);
            this.InitMethods();
        },
        ShowEditInput(index, name, value) {
            this.index = index;
            this.localName = name;
            this.variable_fx = value;
            this.showInputEdit = true;
        },
        updatePropertyHeader() {
            let OldName = this.AllHeaders[this.index].key;
            let OldValue = this.AllHeaders[this.index].value;
            this.AllHeaders[this.index].key = this.key;
            this.AllHeaders[this.index].value = this.value_header;
            this.$emit("updatePropertyHeader", this.key, this.value_header, OldName, OldValue);
        },
        showEditModal(index, name, value) {
            this.index = index;
            this.name_form = name;
            this.value_form = value;
            this.showEdit = true;
        },
        updateProperty() {
            let OldName = this.AllProperties[this.index].name;
            let OldValue = this.AllProperties[this.index].value;
            this.AllProperties[this.index].name = this.name_form;
            this.AllProperties[this.index].value = this.value_form;
            this.$emit("UpdateValue", this.name_form, this.value_form, OldName, OldValue);
        },
        deleteProperty(index) {
            this.AllProperties.splice(index, 1);
            this.$emit('updateProperties', this.AllProperties);
            this.InitMethods();
        },
        AddFn() {
            this.$emit("addFn", this.TypeFx, this.Retries);
            this.InitMethods();
        },
        UpdatePropertyOutput() {
            let OldName = this.AllOutputs[this.index].name;
            let OldValue = this.AllOutputs[this.index].value;
            this.AllOutputs[this.index].name = this.localNameOutPut;
            this.AllOutputs[this.index].value = this.variable_fxOutput;
            this.$emit("UpdatePropertyOutput", this.localNameOutPut, this.variable_fxOutput, OldName, OldValue);
        },
        DeleteOutput(index) {
            this.AllOutputs.splice(index, 1);
            this.$emit('DeleteOutput', this.AllOutputs);
            this.InitMethods();
        },
        isPythonCode(code) {
            try {
                eval(code);
                return true;
            } catch (error) {
                return false;
            }
        },
        ShowEditOutput(index, name, value) {
            this.index = index;
            this.localNameOutPut = name;
            this.variable_fxOutput = value;
            this.showOutPutEdit = true;
        },
        AddOutputs() {
            this.$emit('AddOutputs', this.localNameOutPut, this.variable_fxOutput);
            this.InitMethods();
        },
        checkCode() {
            if (this.isPythonCode(this.code_python) && this.code_python != "") {
                this.error_code = "";
                return true;
            } else {
                this.error_code = "Incorrect Code";
                return false;
            }
        },
        AddCode() {
          //   if (this.checkCode(this.code_python)) {
                this.$emit("AddCodePython", this.code_python);
           // }
        },
        AddTimer(){
            this.$emit("AddTimer", this.Timer);
        }
    },
    data() {
        return {
            error_code: "",
            selected: "prop",
            AllComments: [],
            AllHeaders: [],
            commentInput: "",
            AllProperties: [],
            AllInputs: [],
            name: "",
            index: -1,
            id: "",
            name_form: '',
            showEdit: false,
            value_form: '',
            TypeFx: "",
            Timer:"",
            Retries: "",
            showHeadersEdit: false,
            key: "",
            value_header: "",
            localName: "",
            showInputEdit: false,
            variable_fx: "",
            AllOutputs: [],
            Comments:[],
            localNameOutPut: "",
            variable_fxOutput: "",
            showOutPutEdit: "",
            code_python: ""
        }
    }

}
</script>

<style lang="scss">
.content_prop {
    max-height: 200px;
    overflow-y: scroll;
}

.text {
    font-weight: bold;
}

.header {
    display: flex;
    border: none;
    background-color: transparent;
    font-weight: 300;

    button {
        background-color: transparent;
        border: none;
    }

    .active {
        border-bottom: 2px solid yellow;
    }
}

.comment-section {
    margin-top: 30px;
    max-width: 800px;
    margin: auto;
}

.comment-header {
    display: flex;
    justify-content: space-between;
    margin-bottom: 5px;
}

.comment-user {
    font-weight: bold;
}

.comment-date {
    color: #666;
}

.comment-text {
    padding: 5px;
    background-color: #f2f2f2;
    border-radius: 5px;
    margin-bottom: 10px;
}

.no-comments {
    color: #666;
}

.input-container {
    position: fixed;
    bottom: 0;
    box-shadow: 0 -2px 6px rgba(0, 0, 0, 0.1);
    display: flex;
    padding: 12px 20px;
    gap: 4px;
}

.NameContent {
    display: flex;
    box-shadow: 0 -2px 6px rgba(0, 0, 0, 0.1);
    padding: 12px 20px;
}

.input-container input {
    flex: 1;
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 20px;
}

.input-container button {
    padding: 10px 20px;
    background-color: #4CAF50;
    color: #fff;
    border: none;
    border-radius: 20px;
    cursor: pointer;
}
</style>
