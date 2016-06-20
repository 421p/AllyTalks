<?php

namespace AllyTalks\WebApp;

use AllyTalks\ORM\DoctrineFactory;
use AllyTalks\ORM\Entities\User;

class Model
{
    private $em;

    public function __construct()
    {
        $this->em = DoctrineFactory::buildEntityManagerFromConfig();
    }

    public function getAllUsers()
    {
        return $this->em->createQueryBuilder()
            ->select('u')
            ->from(User::class, 'u')
            ->getQuery()
            ->getResult();
    }

    public function getUserByLogin(string $login) : User
    {
        $user = $this->em->createQueryBuilder()
            ->select('usr')
            ->from(User::class, 'usr')
            ->where('usr.login = :log')
            ->setParameter('log', $login)
            ->getQuery()->getOneOrNullResult();

        if($user) {
            return $user;
        } else {
            throw new \RuntimeException('No user found with current login.');
        }
    }

    public function getUserByToken(string $token) : User
    {
        $user = $this->em->createQueryBuilder()
            ->select('usr')
            ->from(User::class, 'usr')
            ->where('usr.token = :tok')
            ->setParameter('tok', $token)
            ->getQuery()->getOneOrNullResult();

        if($user) {
            return $user;
        } else {
            throw new \RuntimeException('No user found with current token.');
        }
    }

    public function initiateFlushing()
    {
        $this->em->flush();
    }
}
