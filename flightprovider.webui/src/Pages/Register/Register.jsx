﻿import React, { useState } from "react";
import FormInput from "../../componenets/FormInput/FormInput";
import axios from "axios";
import { Formik } from "formik";
import { Link, useNavigate } from "react-router-dom"; // Import useNavigate
import * as Yup from "yup";

function Register() {
    const validationSchema = Yup.object({
        email: Yup.string()
            .email("Geçersiz e-mail adresi")
            .required("Zorunlu alan"),
        password: Yup.string().required("Zorunlu alan"),
        rePassword: Yup.string()
            .oneOf([Yup.ref("password"), null], "Şifreler eşleşmiyor")
            .required("Şifreler eşleşmiyor"),
    });

    const [loginContent, setLoginContent] = useState("");
    const navigate = useNavigate(); // Initialize useNavigate

    async function RegisterUser(values) {
        try {
            const response = await axios.post(
                "https://localhost:7242/api/identity/register",
                {
                    email: values.email,
                    password: values.password,
                }
            );

            if (response.status === 200) { // Check response status
                setLoginContent("Mailinize onay linki gönderilmiştir !");
                setTimeout(() => {
                    navigate("/login"); // Redirect to login page after a short delay
                }, 2000);
            } else {
                setLoginContent("Bir hata oluştu !");
            }
        } catch (error) {
            setLoginContent("Bir hata oluştu !");
            console.log(error);
        }
    }

    return (
        <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
            <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                <img
                    src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
                    alt="Your Company"
                    className="mx-auto h-10 w-auto"
                />
                <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
                    Kayit Olun !
                </h2>
            </div>

            <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
                <Formik
                    initialValues={{ email: "", password: "", rePassword: "" }}
                    validationSchema={validationSchema}
                    onSubmit={(values) => {
                        RegisterUser(values);
                    }}
                >
                    {({ handleSubmit, handleChange, values, errors }) => (
                        <form className="space-y-6" onSubmit={handleSubmit} method="POST">
                            {loginContent !== "" && (
                                <div className="bg-red-500 text-white p-4 rounded-md mb-4">
                                    {loginContent}
                                </div>
                            )}
                            <FormInput
                                setVariable={values.email}
                                nameInput={"email"}
                                onChange={handleChange}
                                typeInput={"email"}
                                labelText={"Email Address"}
                            ></FormInput>
                            {errors.email && <p className="text-red-600">{errors.email}</p>}

                            <FormInput
                                setVariable={values.password}
                                nameInput={"password"}
                                typeInput={"password"}
                                onChange={handleChange}
                                labelText={"Password"}
                            ></FormInput>
                            {errors.password && (
                                <p className="text-red-600">{errors.password}</p>
                            )}

                            <FormInput
                                setVariable={values.rePassword}
                                nameInput={"rePassword"}
                                typeInput={"password"}
                                onChange={handleChange}
                                labelText={"Re-Password"}
                            ></FormInput>
                            {errors.rePassword && (
                                <p className="text-red-600">{errors.rePassword}</p>
                            )}

                            <div>
                                <button
                                    type="submit"
                                    className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                                >
                                    Kayit Ol
                                </button>
                                <Link
                                    to="/login"
                                    className="mt-2 flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                                >
                                    Giriş Yap
                                </Link>
                            </div>
                        </form>
                    )}
                </Formik>
            </div>
        </div>
    );
}

export default Register;
