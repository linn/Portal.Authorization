import React from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import { useSignIn } from '@linn-it/linn-form-components-library';
import Navigation from '../containers/Navigation';
import App from './App';
import 'typeface-roboto';

function Root() {
    useSignIn();

    return (
        <div>
            <div>
                <Navigation />
                <Routes>
                    <Route path="/" element={<Navigate to="/portal-authorization" replace />} />
                    <Route path="/portal-authorization" element={<App />} />
                </Routes>
            </div>
        </div>
    );
}

export default Root;
