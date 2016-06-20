<?php

namespace AllyTalks\WebApp\Controller;

use AllyTalks\WebApp\Application;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\HttpFoundation\Request;

class Registration extends Controller
{
    private $app;
    private $model;
    private $render;

    public function __construct(Application $application)
    {
        $this->app = $application;
        $this->model = $this->app->getModel();
        $this->render = new Render($application);
    }

    /**
     * @controller
     * @method POST
     * @route /post/reg
     *
     * @param Request $request
     * @return Response
     */
    public function registerNewUser(Request $request)
    {
        $nickname = $this->prepareData($request->request->get('nickname'));
        $login = $this->prepareData($request->request->get('login'));
        $password = $this->prepareData($request->request->get('password'));
        $email = $this->prepareData($request->request->get('email'));

        $errors = $this->assertUser($nickname, $login, $password, $email);
        $data = array('nickname' => $nickname, 'login' => $login, 'email' => $email);
        
        if(!$errors) {
            if ($this->model->addUser(array('login' => $login, 'password' => $password, 'nickname' => $nickname, 'email' => $email)))
                return new Response('Thank you for your registration!');
            else
                return new Response('User with such login already exists!');
        } else {
            return $this->render->registrationPageController($errors, $data);
        }
    }

    private function assertUser($nickname, $login, $password, $email)
    {
        $errors = [];
        
        if (empty($nickname)) {
            $errors['nicknameErr']="*Nickname is required";
        }
        if (empty($login)) {
            $errors['loginErr']="*Login is required";
        }
        if (empty($password)) {
            $errors['passwordErr']="*Password is required";
        }
        if (empty($email)) {
            $errors['emailErr'] = "*Email is required";
        } else {
            if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
                $errors['emailErr'] = "*Invalid email format";
            }
        }
        
        return $errors;
    }

    private function prepareData($data)
    {
        $data = trim($data);
        $data = stripslashes($data);
        $data = htmlspecialchars($data);
        return $data;
    }
}
